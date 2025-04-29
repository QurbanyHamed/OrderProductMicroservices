using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Context;
using OrderService.Model;
using System.Text.Json;
using System.Text;

namespace OrderService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderDbContext _db;
		private readonly HttpClient _http;

		public OrderController(OrderDbContext db, IHttpClientFactory factory)
		{
			_db = db;
			_http = factory.CreateClient("ProductService");
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] int productId)
		{
			var response = await _http.GetAsync($"{productId}");
			if (!response.IsSuccessStatusCode) return NotFound("Product not found");

			var product = JsonSerializer.Deserialize<Product>(
				await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (product.Stock < 1) return BadRequest("Out of stock");

			// Reduce stock
			var content = new StringContent(product.Stock - 1 + "", Encoding.UTF8, "application/json");
			await _http.PutAsync($"{productId}/stock", content);

			var order = new Order { ProductId = productId };
			_db.Orders.Add(order);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
		}

		[HttpGet("{id}")]
		public IActionResult GetOrder(int id)
		{
			var order = _db.Orders.Find(id);
			return order is null ? NotFound() : Ok(order);
		}
	}
}
