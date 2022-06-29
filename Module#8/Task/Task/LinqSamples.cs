// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;


// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
			foreach (var x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return all presented in market products")]
        public void Linq2()
		{
			var products =
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("This sample return list customers, whose sum of all orders exceeds X")]
        public void Linq3()
        {
            var list = new List<int>(){1000,10000,100000};
            foreach (var b in list)
            {
                Console.WriteLine($"X - {b}");
                var customers = dataSource.Customers
                    .GroupBy(x => x.CustomerID)
                    .Select(x => new
                    {
                        ID = x.Key,
                        TotalSum = x.SelectMany(i => i.Orders).Sum(i => i.Total)
                    })
                    .Where(i => i.TotalSum > b);

                foreach (var p in customers)
                {
                    ObjectDumper.Write(p);
                }

                Console.WriteLine("------------------------------------");
            }
		}

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("This sample return return supllier's list for each customer")]
        public void Linq4()
        {
            var customers = dataSource.Customers
                .Join(dataSource.Suppliers,
                    x => new { x.City, x.Country },
                    y => new { y.City, y.Country },
                    (x, y) => new
                    {
                        CustomerId = x.CustomerID,
                        SupplierList = y.SupplierName
                    });

            var customers1 = dataSource.Customers
                .Join(dataSource.Suppliers,
                    x => new { x.City, x.Country },
                    y => new { y.City, y.Country },
                    (x, y) => new
                    {
                        CustomerId = x.CustomerID,
                        SupplierList = y.SupplierName
                    }).GroupBy(z => z.SupplierList);

            Console.WriteLine("Supplier's list without group");
            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }

            Console.WriteLine("-----------");

            Console.WriteLine("Supplier's list with group");
            foreach (var p in customers1)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5")]
        [Description("This sample return customers, whose sum of orders exceeds X ")]
        public void Linq5()
        {
            var list = new List<int>() { 1000, 10000, 100000 };
            foreach (var b in list)
            {
                Console.WriteLine($"X - {b}");
                var customers = dataSource.Customers
                    .GroupBy(x => x.CustomerID)
                    .Select(x => new
                    {
                        Id = x.Key,
                        Orders = x.SelectMany(i => i.Orders).Any(i => i.Total > b)
                    }).Where(a => a.Orders);
                
                foreach (var p in customers)
                {
                    ObjectDumper.Write(p);
                }

                Console.WriteLine("------------------------------------");
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return month and year when they became clients")]
        public void Linq6()
        {
            var customers = dataSource.Customers
                .GroupBy(x => x.CustomerID)
                .Select(x => new
                {
                    Id = x.Key,
                    Month = x.SelectMany(i => i.Orders).Select(i => i.OrderDate).FirstOrDefault().Month,
                    Year = x.SelectMany(i => i.Orders).Select(i => i.OrderDate).FirstOrDefault().Year
                });
            
            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 7")]
        [Description("This sample return month and year when they became clients with order")]
        public void Linq7()
        {
            var customers = dataSource.Customers
                .GroupBy(x => x.CustomerID)
                .Select(x => new
                {
                    Id = x.Key,
                    Month = x.SelectMany(i => i.Orders).Select(i => i.OrderDate).FirstOrDefault().Month,
                    Year = x.SelectMany(i => i.Orders).Select(i => i.OrderDate).FirstOrDefault().Year,
                    Sum = x.SelectMany(i => i.Orders).Sum(i => i.Total),
                    Name = x.Select(i => i.CompanyName).FirstOrDefault()
                }).OrderBy(a => a.Year).ThenBy(a => a.Month).ThenByDescending(a => a.Sum).ThenBy(a => a.Name);

            foreach (var p in customers)
            {
                if(p.Year != 1)
                    ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 8")]
        [Description("This sample return month and year when they became clients")]
        public void Linq8()
        {
            var customers = dataSource.Customers
                .Where(x => (x.PostalCode?.Any(char.IsLetter) ?? false) || string.IsNullOrEmpty(x.Region) || x.Phone.Contains("(")).Distinct();
            
            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 9")]
        [Description("This sample return list of products grouped by category, grouped by stock and sorted in ascending order")]
        public void Linq9()
        {
            var customers = dataSource.Products
                .GroupBy(x => x.Category)
                .Select(x => new
                {
                    Category = x.Key,
                    UnitsInStock = x.GroupBy(i => i.UnitsInStock)
                        .Select(i => new
                        {
                            Price = i.OrderBy(m => m.UnitPrice)
                        })
                });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 10")]
        [Description("This sample return list of products grouped by price")]
        public void Linq10()
        {
            var customers = dataSource.Products
                .GroupBy(i =>
                    i.UnitPrice < 10 ? "Дешевые" :
                    i.UnitPrice < 15 ? "Среднее" :
                    "Дорогие");
            

            foreach (var p in customers)
            {
                ObjectDumper.Write(p.Key);
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 11")]
        [Description("This sample return list of city with average return and average intensity")]
        public void Linq11()
        {
            var customers = dataSource.Customers
                .GroupBy(i => i.City)
                .Select(i => new
                {
                    City = i.Key,
                    AverageReturn = (float) i.SelectMany(x => x.Orders).Average(x => x.Total),
                    AverageIntensity = i.Average(x => x.Orders.Length)
                });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 12")]
        [Description("This sample return purchase statistics by month, year, year and month")]
        public void Linq12()
        {
            var statMonth = dataSource.Customers
                .SelectMany(x => x.Orders)
                .GroupBy(x => x.OrderDate.Month)
                .Select(x => new
                {
                    Month = x.Key,
                    Count = x.Select(i => i.OrderID).Count()
                }).OrderBy(i => i.Month);

            var statYear = dataSource.Customers
                .SelectMany(x => x.Orders)
                .GroupBy(x => x.OrderDate.Year)
                .Select(x => new
                {
                    Year = x.Key,
                    Count = x.Select(i => i.OrderID).Count()
                }).OrderBy(i => i.Year);

            var statYearMonth = dataSource.Customers
                .SelectMany(x => x.Orders)
                .GroupBy(x => new
                {
                    x.OrderDate.Year,
                    x.OrderDate.Month
                })
                .Select(x => new
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Count = x.Select(i => i.OrderID).Count()
                }).OrderBy(i => i.Year).ThenBy(i => i.Month);

            foreach (var m in statMonth)
            {
                ObjectDumper.Write(m);
            }

            Console.WriteLine();

            foreach (var y in statYear)
            {
                ObjectDumper.Write(y);
            }

            Console.WriteLine();

            foreach (var ym in statYearMonth)
            {
                ObjectDumper.Write(ym);
            }

        }
    }
}
