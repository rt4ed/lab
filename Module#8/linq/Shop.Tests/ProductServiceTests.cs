using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shop.Enums;
using Shop.Models;

namespace Shop.Tests
{
	[TestFixture]
	public class ProductServiceTests : BaseServiceTests
	{
		[Test]
		public void SortByPrice_Ascending_ProductsAreSortedAsc()
		{
			SeedDefault();
			var service = GetProductService();
			var products = service.SortByPrice(SortOrder.Ascending).ToList();

			for (int i = 0; i < products.Count - 1; i++)
			{
				Assert.LessOrEqual(products[i].Price, products[i + 1].Price);
			}
		}

		[Test]
		public void SortByPrice_Descending_ProductsAreSortedDesc()
		{
			SeedDefault();
			var service = GetProductService();
			var products = service.SortByPrice(SortOrder.Descending).ToList();

			for (int i = 0; i < products.Count - 1; i++)
			{
				Assert.GreaterOrEqual(products[i].Price, products[i + 1].Price);
			}
		}

		[TestCase("Пылесос")]
		[TestCase("Утюг")]
		public void FilterByNameStart_NameOfProduct_ReturnsProductsThatStartWithString(string startsWith)
		{
			SeedDefault();
			var category = DbContext.Categories.First();
			var expectedIdSelectors = new List<Func<int>>();
			for (int i = 0; i < 20; i++)
			{
				var product = new Product
				{
					Name = i % 4 == 0 ? $"{startsWith}{Guid.NewGuid()}" : Guid.NewGuid().ToString(),
					Category = category
				};
				if(i % 4 == 0)
					expectedIdSelectors.Add(() => product.Id);
			}

			DbContext.SaveChanges();

			var service = GetProductService();
			var products = service.FilterByNameStart(startsWith).ToList();

			foreach (var product in products)
			{
				Assert.True(expectedIdSelectors.Select(i => i()).Contains(product.Id));
			}
		}

		[Test]
		public void GroupByVendor_SomeVendors_ReturnsVendorsAndTheirProducts()
		{
			SeedDefault();
			var service = GetProductService();
			var groups = service.GroupByVendor();

			foreach (var group in groups)
			{
				Assert.True(group.Value.All(product => product.Vendor == group.Key));
			}
		}

		[Test]
		public void GetTheMostExpensiveProducts__ReturnsTheMostExpensiveProducts()
		{
			SeedDefault();
			var newProducts = new[]
			{
				new Product {CategoryId = 1, Price = decimal.MaxValue},
				new Product {CategoryId = 1, Price = decimal.MaxValue},
			};
			DbContext.Products.AddRange(newProducts);
			DbContext.SaveChanges();

			var service = GetProductService();
			var theMostExpensiveProducts = service.GetTheMostExpensiveProducts().ToArray();

			CollectionAssert.AreEqual(newProducts.Select(p => p.Id).OrderBy(i => i), theMostExpensiveProducts.Select(p => p.Id).OrderBy(i => i));
		}

		[Test]
		public void GetTheCheapestProducts__ReturnsTheCheapestProducts()
		{
			SeedDefault();
			var newProducts = new[]
			{
				new Product {CategoryId = 1, Price = decimal.MinValue},
				new Product {CategoryId = 1, Price = decimal.MinValue},
			};
			DbContext.Products.AddRange(newProducts);
			DbContext.SaveChanges();

			var service = GetProductService();
			var theMostExpensiveProducts = service.GetTheCheapestProducts().ToArray();

			CollectionAssert.AreEqual(newProducts.Select(p => p.Id).OrderBy(i => i), theMostExpensiveProducts.Select(p => p.Id).OrderBy(i => i));
		}

		[TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, ExpectedResult = 5)]
		[TestCase(1, 2, 3, ExpectedResult = 2)]
		[TestCase(5, 7, 9, ExpectedResult = 7)]
		public decimal GetAverageProductPrice__ReturnsAverageProductPrice(params int[] prices)
		{
			var category = new Category();
			foreach (var price in prices)
			{
				DbContext.Products.Add(new Product {Category = category, Price = price});
			}

			DbContext.SaveChanges();

			var service = GetProductService();

			var averagePrice = service.GetAverageProductPrice();

			return averagePrice;
		}

		[TestCase(5, 1, 2, 3, 4, 5, 6, 7, 8, 9, ExpectedResult = 5)]
		[TestCase(6, 1, 2, 3, ExpectedResult = 2)]
		[TestCase(7, 5, 7, 9, ExpectedResult = 7)]
		public decimal GetAverageProductPriceInCategory__ReturnsAverageProductPriceInCategory(int categoryId, params int[] prices)
		{
			SeedDefault();
			DbContext.Categories.Add(new Category {Id = categoryId});
			foreach (var price in prices)
			{
				DbContext.Products.Add(new Product {CategoryId = categoryId, Price = price});
			}

			DbContext.SaveChanges();

			var service = GetProductService();

			var averagePrice = service.GetAverageProductPriceInCategory(categoryId);

			return averagePrice;
		}

		[TestCase(100, 5, ExpectedResult = 95)]
		[TestCase(100, 25, ExpectedResult = 75)]
		[TestCase(100, 90, ExpectedResult = 10)]
		[TestCase(100, 0, ExpectedResult = 100)]
		public decimal GetProductsWithActualPrice__ReturnsExpectedValues(decimal initialPrice, int discount)
		{
			var category = new Category();
			var product = new Product {Category = category, Price = initialPrice, Discount = discount};
			DbContext.Products.Add(product);
			DbContext.SaveChanges();

			var service = GetProductService();
			var products = service.GetProductsWithActualPrice();
			return products.Single().Value;
		}

		[Test]
		public void GetGroupedByVendorAndCategoryProducts__ReturnsGroupedByVendorAndCategoryProductsOrderedDescendingByPrice()
		{
			SeedDefault();
			var service = GetProductService();
			var result = service.GetGroupedByVendorAndCategoryProducts();

			foreach (var vendorProductsDto in result)
			{
				Assert.IsTrue(vendorProductsDto.CategoryProducts.All(pair => pair.Value.All(p => p.Vendor == vendorProductsDto.Vendor)));

				foreach (var categoryProducts in vendorProductsDto.CategoryProducts)
				{
					Assert.IsTrue(categoryProducts.Value.All(p => p.Category.Name == categoryProducts.Key));

					if (categoryProducts.Value.Count > 0)
						for (int i = 0; i < categoryProducts.Value.Count - 1; i++)
						{
							Assert.GreaterOrEqual(categoryProducts.Value[i].Price, categoryProducts.Value[i + 1].Price);
						}
				}
			}
		}

		[Test]
		public void UpdateDiscountIfUnitsInStockEquals1AndGetUpdatedProducts__ReturnsExpectedProducts()
		{
			SeedDefault();

			var service = GetProductService();
			var result = service.UpdateDiscountIfUnitsInStockEquals1AndGetUpdatedProducts(90).ToList();

			Assert.True(result.All(p => p.UnitsInStock <= 2));
			Assert.True(result.All(p => p.Discount == 90));

			CollectionAssert.AreEqual(new []{6, 7, 8, 9, 10}, result.OrderBy(p => p.Id).Select(p => p.Id));
		}

		private ProductService GetProductService()
		{
			return new ProductService(DbContext);
		}
	}
}