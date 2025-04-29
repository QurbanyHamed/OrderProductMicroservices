namespace OrderService.Model
{
	public class Order
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
