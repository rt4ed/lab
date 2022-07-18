using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryNetwork;
using LibraryNetwork.Core;
using System.IO;
using LibraryNetwork.Classes;

namespace LibraryNetworkTests
{
    [TestFixture]
    public class LibraryTests
    {

        private Book GetBook()
        {
            Book book = new Book(1, "CLR via C#", 1000, new DateTime(2000, 1, 1), 100, "none", "Jeffrey Richter",
                "London", "MDN", 12, "123-123");

            return book;
        }

        private Newspaper GetNewspaper()
        {
            Newspaper newspaper = new Newspaper(2, "CLR", 1000, new DateTime(2001, 1, 1), 12, "none",
                "London", "MSA", 12, 1, DateTime.Now, "123-123");
            return newspaper;
        }

        private Patent GetPatent()
        {
            Patent patent = new Patent(3, "Patent", 100, new DateTime(2002, 1, 1), 111, "Rx", "USA", 33, new DateTime(2002, 1, 1), "312");

            return patent;
        }


        [Test]
        public void Add_StandartItems_WorkAsExpected3items()
        {
            Book book = GetBook();
            Newspaper newspaper = GetNewspaper();
            Patent patent = GetPatent();
            Storage storage = new Storage();
            Library library = new Library(storage);

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
            Library library = new Library(storage);

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
            Library library = new Library(storage);

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
            Library library = new Library(storage);

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
            Library library = new Library(storage);

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
            Library library = new Library(storage);

            library.Add(book);
            library.Add(book1);
            library.Add(book2);
            library.Add(newspaper);
            library.Add(patent);

            var expectedDictionary = new Dictionary<string, ICollection<Book>>(); ;
            expectedDictionary.Add(book.PublisherName, new List<Book>());
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
            Library library = new Library(storage);

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

        [Test]
        public void Search_StandartObject_WorkAsExpected()
        {
            Patent patent = GetPatent();
            Storage storage = new Storage();
            Library library = new Library(storage);

            library.Add(patent);

            Func<BaseStorageObject, bool> predicate = (obj) => obj.Id == patent.Id;
            var actualCollection = library.Search<BaseStorageObject>(predicate);

            var expectedCollection = new List<BaseStorageObject>();
            expectedCollection.Add(patent);

            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void Filter_StandartObject_WorkAsExpected()
        {
            Patent patent = GetPatent();
            Book book = GetBook();
            Storage storage = new Storage();
            Library library = new Library(storage);

            library.Add(patent);
            library.Add(book);


            var actualCollection = library.Filter<BaseStorageObject>((x, y) => x.Id.CompareTo(y.Id));

            var expectedCollection = new List<BaseStorageObject>();
            expectedCollection.Add(book);
            expectedCollection.Add(patent);

            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        [Test]
        public void ObjectEdit_StandartObject_WorkAsExpected()
        {
            Patent patent = GetPatent();
            Patent patent1 = GetPatent();
            patent1.Title = "Title";
            Storage storage = new Storage();
            Library library = new Library(storage);

            library.Add(patent);

            library.ObjectEdit(3, "Title", "Title");

            Assert.AreEqual(patent.Title, patent1.Title);
        }

        [Test]
        public void CreateReport_StandartObject_WorkAsExpected()
        {
            Patent patent = GetPatent();
            Book book = GetBook();
            Book book1 = GetBook();
            book1.Title = "Book of recipes";
            Newspaper newspaper = GetNewspaper();
            Storage storage = new Storage();
            Library library = new Library(storage);
            TextReport textReport = new TextReport();
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            var reportPath = path + @"\LibraryNetwork\ReportFiles\Report.txt";
            var reportTestPath = path + @"\LibraryNetworkTests\ReportFilesTests\ReportTest.txt";

            library.Add(patent);
            library.Add(book);
            library.Add(newspaper);
            library.Add(book1);

            library.CreateReport(reportPath, textReport);

            var expectedFile = new FileInfo(reportTestPath);
            var actualFile = new FileInfo(reportPath);

            FileAssert.AreEqual(expectedFile, actualFile);
        }

        [Test]
        public void CreateReport_StandartObject_ThrowDirectoryNotFoundException()
        {
            Patent patent = GetPatent();
            Storage storage = new Storage();
            Library library = new Library(storage);
            TextReport textReport = new TextReport();
            var reportPath = @"D\sda\dsad\ads";

            library.Add(patent);

            Assert.Throws<DirectoryNotFoundException>(() => library.CreateReport(reportPath, textReport));
        }
    }
}
