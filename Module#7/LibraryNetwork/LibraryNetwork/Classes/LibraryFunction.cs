using LibraryNetwork.Interfaces;
using System.Collections.Generic;
using LibraryNetwork.Classes;

namespace LibraryNetwork
{
    public class LibraryFunction 
    {
        private readonly IStorage _storage;
        
        public LibraryFunction(IStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Добавление объекта в каталог
        /// </summary>
        /// <param name="obj"></param>
        public void Add<T>(T obj) where T : BaseStorageObject
        {
            _storage.Add(obj);
        }

        /// <summary>
        /// Удаление объекта из каталога
        /// </summary>
        /// <param name="obj"></param>
        public void Remove<T>(T obj) where T : BaseStorageObject
        {
            _storage.Delete(obj);
        }

        /// <summary>
        /// Показать все объекты каталога
        /// </summary>
        public IEnumerable<BaseStorageObject> GetAllObject<T>()
        {
            foreach (var item in _storage.GetStorageObjects<BaseStorageObject>())
                if (item is T)
                    yield return item;
        }

        /// <summary>
        /// Поиск объектов по названию
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Возвращает массив объктов, название которых содержит в себе искомую подстроку</returns>
        public ICollection<BaseStorageObject> SearchByTitle(string title)
        {
            var containsList = new List<BaseStorageObject>();

            foreach (var a in GetAllObject<BaseStorageObject>())
            {
                if (a.Title.Contains(title))
                {
                    containsList.Add(a);
                }
            }

            return containsList;
        }

        /// <summary>
        /// Сортирует массив объектов по году издания
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Возвращает отсортированный список, в зависимости от направления сортировки</returns>
        public IList<BaseStorageObject> SortByYear(bool direction)
        {
            var list = _storage.GetStorageObjects<BaseStorageObject>();

            if (direction)
            {
                list.Sort(new ForwardComparer<BaseStorageObject>());
            }
            else
            {
                list.Sort(new ReverseComparer<BaseStorageObject>());
            }

            return list;
        }

        /// <summary>
        /// Поиск книги по автору
        /// </summary>
        /// <param name="author"></param>
        /// <returns>Возвращает массив объектов "Book"</returns>
        public ICollection<BaseStorageObject> SearchByAuthor(string author)
        {
            var list = new List<BaseStorageObject>();

            foreach (Book item in GetAllObject<Book>())
            {
                if (item.Authors.Contains(author))
                {
                    list.Add(item);
                }    
            }

            return list;
        }

        /// <summary>
        /// Поиск объектов - Book, издатель которых начинаются с определённого набора символов, а затем их группировка по издателю
        /// </summary>
        /// <param name="publisherName"></param>
        /// <returns>Возвращает массив сгруппированных объектов Book</returns>
        public IDictionary<string, ICollection<Book>> GetBookWithGroup(string publisherName)
        {
            
            Dictionary<string, ICollection<Book>> dictionary = new Dictionary<string, ICollection<Book>>();
            
            foreach (Book book in GetAllObject<Book>())
            {
                if (book.PublisherName.StartsWith(publisherName))
                {
                    if (!dictionary.ContainsKey(book.PublisherName))
                        dictionary.Add(book.PublisherName, new List<Book>());

                    dictionary[book.PublisherName].Add(book);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Группировка объектов по году публикации
        /// </summary>
        /// <returns>Возвращает массив сгруппированных объектов</returns>
        public IDictionary<int, ICollection<BaseStorageObject>> GroupOdjects()
        {
            Dictionary<int, ICollection<BaseStorageObject>> dictionary = new Dictionary<int, ICollection<BaseStorageObject>>();

            foreach (var baseObj in _storage.GetStorageObjects<BaseStorageObject>())
            {
                if(!dictionary.ContainsKey(baseObj.YearOfPublish.Year))
                    dictionary.Add(baseObj.YearOfPublish.Year, new List<BaseStorageObject>());

                dictionary[baseObj.YearOfPublish.Year].Add(baseObj);
            }

            return dictionary;
        }
    }
}
