using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shop.Models;

namespace Shop.Tests
{
	public class BaseServiceTests
	{
		protected ShopContext DbContext;

		[SetUp]
		public void Setup()
		{
			var builder = new DbContextOptionsBuilder();
			builder.UseSqlite("Filename=:memory:");
			DbContext = new ShopContext(builder.Options);

			DbContext.Database.OpenConnection();

			DbContext.Database.EnsureDeleted();
			DbContext.Database.EnsureCreated();
		}

		protected void SeedDefault()
		{
			for (int i = 1; i <= 3; i++)
			{
				var category = new Category
				{
					Id = i,
					Name = $"Category{i}",
					Description = $"CategoryDescription{i}"
				};
				DbContext.Categories.Add(category);
			}

			for (int i = 1; i <= 15; i++)
			{
				var product = new Product
				{
					Name = $"Product{i}",
					Price = i % 5 == 0 ? 50 : i % 5 * 10,
					Discount = i % 3,
					UnitsInStock = (15 - i) % 3,
					Vendor = $"Vendor{i % 2}",
					CategoryId = i % 3 == 0 ? 3: i % 3
				};
				DbContext.Products.Add(product);
			}

			DbContext.SaveChanges();
		}
	}
}