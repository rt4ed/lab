using LibraryNetwork.Interfaces;
using System;
using System.Linq;

namespace LibraryNetwork
{
    public class LibraryFunction 
    {
        private IStorage _storage;
        
        public LibraryFunction(IStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Добавление объекта в каталог
        /// </summary>
        /// <param name="obj"></param>
        public void Add(BaseStorageObject obj)
        {
            _storage.Add(obj);
        }

        /// <summary>
        /// Удаление объекта из каталога
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(BaseStorageObject obj)
        {
            _storage.Delete(obj);
        }

        /// <summary>
        /// Показать все объекты каталога
        /// </summary>
        public BaseStorageObject[] GetAllObject()
        {
            return _storage.GetStorageObjects();
        }

        /// <summary>
        /// Поиск объектов по названию
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Возвращает массив объктов, название которых содержит в себе искомую подстроку</returns>
        public BaseStorageObject [] SearchByTitle(string title)
        {
            var mas = _storage.GetStorageObjects();
            BaseStorageObject[] reservStorage = new BaseStorageObject[mas.Length];
            int i = 0;
            foreach (var a in mas)
            {
                if (a.Title.Contains(title))
                {
                    reservStorage[i] = a;
                    i++;
                }   
            }
            Array.Resize(ref reservStorage, i);
            return reservStorage;
        }

        /// <summary>
        /// Сортирует массив объектов по году издания
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Возвращает отсортированный список, в зависимости от направления сортировки</returns>
        public BaseStorageObject[] SortByYear(bool direction)
        {
            var arr = _storage.GetStorageObjects();
            Array.Sort(arr);

            if(!direction)
                Array.Reverse(arr);

            return arr;
        }

        /// <summary>
        /// Поиск книги по автору
        /// </summary>
        /// <param name="author"></param>
        /// <returns>Возвращает массив объектов "Book"</returns>
        public Book[] SearchByAuthor(string author)
        {
            var arr = _storage.GetStorageObjects();
            var books = new Book[arr.Length];
            int i = 0;

            foreach (var b in arr)
            {
                if(b is Book book)
                {
                    if (book.Authors.Contains(author))
                    {
                        books[i] = book;
                        i++;
                    }
                } 
            }

            Array.Resize(ref books, i);
            return books;
        }

        /// <summary>
        /// Поиск объектов - Book, издатель которых начинаются с определённого набора символов, а затем их группировка по издателю
        /// </summary>
        /// <param name="publisherName"></param>
        /// <returns>Возвращает массив сгруппированных объектов Book</returns>
        public IGrouping<string,Book>[] GetBookWithGroup(string publisherName)
        {
            var arr = _storage.GetStorageObjects();
            var books = new Book[arr.Length];
            int i = 0;

            foreach (var b in arr)
            {
                if (b is Book book)
                {
                    if (book.PublisherName.StartsWith(publisherName))
                    {
                        books[i] = book;
                        i++;
                    }
                }
            }

            Array.Resize(ref books, i);

            var groupBooks = books.GroupBy(g => g.PublisherName).ToArray();

            return groupBooks;
        }

        /// <summary>
        /// Группировка объектов по году публикации
        /// </summary>
        /// <returns>Возвращает массив сгруппированных объектов</returns>
        public IGrouping<int, BaseStorageObject>[] GroupOdjects()
        {
            var arr = _storage.GetStorageObjects();

            var groupObj = arr.GroupBy(b => b.YearOfPublish.Year);

            return groupObj.ToArray();
        }
    }
}
