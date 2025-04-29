using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Context
{
	public class ProductDbContext : DbContext
	{
		public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

		public DbSet<Product> Products => Set<Product>();
	}
}
