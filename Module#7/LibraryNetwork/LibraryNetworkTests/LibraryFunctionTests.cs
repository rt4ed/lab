using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryNetwork;

namespace LibraryNetworkTests
{
    [TestFixture]
    public class LibraryFunctionTests
    {
        
        private Book GetBook()
        {
            Book book = new Book(1, "CLR via C#", 1000, "Jeffrey Richter",
                new DateTime(2000,1,1), "London", "MDN", "No","123-123");

            return book;
        }

        private Newspaper GetNewspaper()
        {
            Newspaper newspaper = new Newspaper(1, "CLR", 1000, new DateTime(2001,1,1),
                "London", "MSA", "No", 1, DateTime.Now, "123-123");

            return newspaper;
        }

        private Patent GetPatent()
        {
            Patent patent = new Patent(1, "Patent", 111, new DateTime(2002,1,1),
                "MS", "Russia", 18, DateTime.Now, "No");

            return patent;
        }
        
        [Test]
        public void Add_StandartItems_WorkAsExpected3items()
        {
            Book book = GetBook();
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(newspaper);
            library.Add(patent);

            int expectedCount = 3;

            var expectedArray = new BaseStorageObject[3];
            expectedArray[0] = book;
            expectedArray[1] = newspaper;
            expectedArray[2] = patent;

            int actualCount = library.GetAllObject<BaseStorageObject>().Count();
            var actualArray = library.GetAllObject<BaseStorageObject>();

            Assert.AreEqual(expectedCount, actualCount);
            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void Delete_StandartItems_WorkAsExpected2items()
        {
            Book book = GetBook();
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(newspaper);
            library.Add(patent);
            library.Remove(newspaper);

            int expectedCount = 2;
            var expectedArray = new BaseStorageObject[2];
            expectedArray[0] = book;
            expectedArray[1] = patent;

            int actualCount = library.GetAllObject<BaseStorageObject>().Count();
            var actualArray = library.GetAllObject<BaseStorageObject>();

            Assert.AreEqual(expectedCount, actualCount);
            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void Search_StandartTitle_WorkAsExpected2items()
        {
            Book book = GetBook();
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(newspaper);
            library.Add(patent);
            
            var expectedArray = new BaseStorageObject[2];
            expectedArray[0] = book;
            expectedArray[1] = newspaper;

            var actualArray = library.SearchByTitle("CLR");
            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void SortByYear_SearchCurrentYear_WorkAsExpected()
        {
            Book book = GetBook();
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(newspaper);
            library.Add(patent);

            var expectedArray1 = new BaseStorageObject[3];
            expectedArray1[0] = book;
            expectedArray1[1] = newspaper;
            expectedArray1[2] = patent;

            var expectedArray2 = new BaseStorageObject[3];
            expectedArray2[0] = patent;
            expectedArray2[1] = newspaper;
            expectedArray2[2] = book;

            var actualArray1 = library.SortByYear(true);
            CollectionAssert.AreEqual(expectedArray1, actualArray1);

            var actualArray2 = library.SortByYear(false);
            CollectionAssert.AreEqual(expectedArray2, actualArray2);
        }

        [Test]
        public void SearchByAuthor_SearchStandartTitle_WorkAsExpected2item()
        {
            Book book = GetBook();
            Book book1 = GetBook();
            book1.Authors = "Richter";
            Book book2 = GetBook();
            book2.Authors = "Pushkin";
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(book1);
            library.Add(book2);
            library.Add(newspaper);
            library.Add(patent);

            var expectedArray = new List<BaseStorageObject>();
            expectedArray.Add(book);
            expectedArray.Add(book1);
            
            var actualArray = library.SearchByAuthor("Richter");
            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void GetBookWithGroup_StandartPublisher_WorkAsExpected2itemAnd1item()
        {
            Book book = GetBook();
            Book book1 = GetBook();
            book1.PublisherName = "MMM";
            Book book2 = GetBook();
            book2.PublisherName = "DMM";
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(book1);
            library.Add(book2);
            library.Add(newspaper);
            library.Add(patent);

            var expectedDictionary = new Dictionary<string, ICollection<Book>>(); ;
            expectedDictionary.Add(book.PublisherName,new List<Book>());
            expectedDictionary.Add(book1.PublisherName, new List<Book>());
            expectedDictionary[book.PublisherName].Add(book);
            expectedDictionary[book1.PublisherName].Add(book1);

            var expectedDictionary1 = new Dictionary<string, ICollection<Book>>(); ;
            expectedDictionary1.Add(book2.PublisherName, new List<Book>());
            expectedDictionary1[book2.PublisherName].Add(book2);

            var actualArray = library.GetBookWithGroup("M");
            CollectionAssert.AreEqual(expectedDictionary, actualArray);
            
            var actualArray1 = library.GetBookWithGroup("D");
            CollectionAssert.AreEqual(expectedDictionary1, actualArray1);
        }

        [Test]
        public void GroupObjects_StandartYear_WorkAsExpected()
        {
            Book book = GetBook();
            Book book1 = GetBook();
            Book book2 = GetBook();
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            LibraryFunction library = new LibraryFunction(storage);

            library.Add(book);
            library.Add(book1);
            library.Add(book2);
            library.Add(newspaper);
            library.Add(patent);

            var expectedDictionary = new Dictionary<int, ICollection<BaseStorageObject>>();
            expectedDictionary.Add(book.YearOfPublish.Year, new List<BaseStorageObject>());
            expectedDictionary.Add(newspaper.YearOfPublish.Year, new List<BaseStorageObject>());
            expectedDictionary.Add(patent.YearOfPublish.Year, new List<BaseStorageObject>());

            expectedDictionary[book.YearOfPublish.Year].Add(book);
            expectedDictionary[book1.YearOfPublish.Year].Add(book1);
            expectedDictionary[book2.YearOfPublish.Year].Add(book2);
            expectedDictionary[newspaper.YearOfPublish.Year].Add(newspaper);
            expectedDictionary[patent.YearOfPublish.Year].Add(patent);
            
            var actualArray = library.GroupOdjects();
            CollectionAssert.AreEqual(expectedDictionary, actualArray);
        }
    }
}
