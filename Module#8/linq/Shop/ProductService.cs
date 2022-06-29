using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Dto;
using Shop.Enums;
using Shop.Models;

namespace Shop
{
	public class ProductService
	{
		private readonly ShopContext _dbContext;

		private IList<Product> Products => _dbContext.Products.Include(p => p.Category).ToList();
		
		public ProductService(ShopContext dbContext)
		{
			_dbContext = dbContext;
		}

		/// <summary>
		/// Сортирует товары по цене и возвращает отсортированный список
		/// </summary>
		/// <param name="sortOrder">Порядок сортировки</param>
		public IEnumerable<Product> SortByPrice(SortOrder sortOrder)
		{
			var selectProd = Products.OrderBy(e => e.Price);
			switch (sortOrder)
			{
				case SortOrder.Ascending:
					selectProd = Products.OrderBy(e => e.Price);
					break;
				case SortOrder.Descending:
					selectProd = Products.OrderByDescending(e => e.Price);
					break;
			}

			return selectProd;
		}

		/// <summary>
		/// Возвращает товары, название которых начинается на <see cref="name"/>
		/// </summary>
		/// <param name="name">Фильтр - строка, с которой начинается название товара</param>
		public IEnumerable<Product> FilterByNameStart(string name)
		{
			var Prod = Products.Where(e => e.Name.StartsWith(name));
			return Prod;
		}

		/// <summary>
		/// Группирует товары по производителю
		/// </summary>
		public IDictionary<string, List<Product>> GroupByVendor()
		{
            var Prod1 = Products.GroupBy(e => e.Vendor).ToDictionary(g => g.Key, g => g.ToList());
			return Prod1;
		}

		/// <summary>
		/// Возвращает список самых дорогих товаров (самые дорогие - товары с наибольшей ценой среди всех товаров)
		/// </summary>
		public IEnumerable<Product> GetTheMostExpensiveProducts()
		{ 
            var Prod1 = Products.Max(e => e.Price);
			var Prod2 = Products.Where(e => e.Price == Prod1);
            return Prod2;
        }

        /// <summary>
        /// Возвращает список самых дешевых товаров (самые дешевые - товары с наименьшей ценой среди всех товаров)
        /// </summary>
        public IEnumerable<Product> GetTheCheapestProducts()
		{
			var Prod1 = Products.Min(e => e.Price);
			var Prod2 = Products.Where(e => e.Price == Prod1);
			return Prod2;
		}

		/// <summary>
		/// Возвращает среднюю цену среди всех товаров
		/// </summary>
		public decimal GetAverageProductPrice()
		{

			var Prod = Products.Average(e => e.Price);
			return Prod;
		}

		/// <summary>
		/// Возвращает среднюю цену товаров в указанной категории
		/// </summary>
		/// <param name="categoryId">Идентификатор категории</param>
		public decimal GetAverageProductPriceInCategory(int categoryId)
		{
			var Prod = Products.Where(e=> e.CategoryId==categoryId).Average(e => e.Price);
			return Prod;
		}

		/// <summary>
		/// Возвращает список продуктов с актуальной ценой (после применения скидки)
		/// </summary>
		public IDictionary<Product, decimal> GetProductsWithActualPrice()
		{
			var Pr = new Product();
            foreach (var p in Products)
            {
                p.Price -= p.Discount;
            }
			var Prod = Products.ToDictionary(g => Pr, g => g.Price);
            return Prod;
		}

		/// <summary>
		/// Возвращает список продуктов, сгруппированный по производителю, а внутри - по названию категории.
		/// Продукты внутри последней группы отсортированы в порядке убывания цены
		/// </summary>
		public IList<VendorProductsDto> GetGroupedByVendorAndCategoryProducts()
		{
            var Prod = Products
                .GroupBy(p => p.Vendor)
                .Select(g => new VendorProductsDto
                {
                    Vendor = g.Key,
                    CategoryProducts = g
					.GroupBy(f => f.Category.Name)
					.ToDictionary(a => a.Key, a => a.OrderByDescending(z => z.Price).ToList()) 
                });

            return Prod.ToList();
		}

		/// <summary>
		/// Обновляет скидку на товары, которые остались на складе в количестве 1 шт,
		/// и возвращает список обновленных товаров
		/// </summary>
		/// <param name="newDiscount">Новый процент скидки</param>
		public IEnumerable<Product> UpdateDiscountIfUnitsInStockEquals1AndGetUpdatedProducts(int newDiscount)
		{
			
			foreach(var p in Products.Where(e => e.UnitsInStock == 1))
            {
				p.Discount = newDiscount;
            }
			var Prod = Products.Where(e => e.Discount == 90);

			return Prod;
		}
	}
}
