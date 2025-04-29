using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Context;
using ProductService.Models;

namespace ProductService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ProductDbContext _context;

		public ProductController(ProductDbContext context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var product = _context.Products.Find(id);
			return product is null ? NotFound() : Ok(product);
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			_context.Products.Add(product);
			_context.SaveChanges();
			return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
		}

		[HttpPut("{id}/stock")]
		public IActionResult UpdateStock(int id, [FromBody] int newStock)
		{
			var product = _context.Products.Find(id);
			if (product == null) return NotFound();

			product.Stock = newStock;
			_context.SaveChanges();
			return NoContent();
		}
	}
}
