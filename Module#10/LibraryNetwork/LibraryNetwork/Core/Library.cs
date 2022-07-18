using LibraryNetwork.Classes;
using LibraryNetwork.EventHandler;
using LibraryNetwork.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibraryNetwork
{
    public class Library
    {
        private IStorage _storage;
        private IGetData _getData;
        private ILogger _logger;

        public Library(IStorage storage, IGetData getData, ILogger logger)
        {
            _logger = logger;
            _storage = storage;
            _getData = getData;
            _storage.FundAdded += ObjectAdd;
            _storage.FundDeleting += ObjectDelete;
        }

        /// <summary>
        /// Сохранение файлов
        /// </summary>
        public void Save(IEnumerable<BaseStorageObject> baseStorageObjects, string path)
        {
            var manager = _getData.GetDataManager(path);
            manager.SaveData(baseStorageObjects, path);
        }

        /// <summary>
        /// Загрузка файлов
        /// </summary>
        public void Load(IValidation validation, string path, bool safe)
        {
            var manager = _getData.GetDataManager(path);
            var data = manager.GetData(path);
            ICollection<ValidationResult> validationResult;
            bool correct = true;
            if (safe)
            {
                foreach(var d in data)
                {
                    if (!validation.IsValid(d, out validationResult))
                    {
                        correct = false;
                        break;
                    }
                }
                if (correct)
                {
                    foreach(var a in GetAllObject<BaseStorageObject>())
                    {
                        Remove<BaseStorageObject>(a);
                    }
                    foreach(var d in data)
                    {
                        Add<BaseStorageObject>(d);
                    }
                }
            }
            else
            {
                foreach (var a in GetAllObject<BaseStorageObject>())
                {
                    Remove<BaseStorageObject>(a);
                }

                foreach (var d in data)
                {
                    if (!validation.IsValid(d, out validationResult))
                    {
                        _logger.Info(validationResult);
                    }
                    Add<BaseStorageObject>(d);
                }
            }
        }

        /// <summary>
        /// Создание отчёта с произвольной стратегией создания
        /// </summary>
        /// <param name="path"></param>
        /// <param name="reportStrategy"></param>
        public void CreateReport(string path, IReport reportStrategy)
        {
            var report = new Report(path, reportStrategy);
            report.Create(GetAllObject<BaseStorageObject>());
        }

        /// <summary>
        /// Обработчик события "FundAdded", выводит сообщение, содержащее тип добавленного объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ObjectAdd(object sender, BaseStorageObjectArgs e)
        {
            Console.WriteLine($"Object {e.ObjectType} added.");
        }

        /// <summary>
        /// Обработчик события "FundDeleting", выводит сообщение, содержащее информацию об объекте,
        /// а также должен предоставлять возможность отменнить удаление( но не предоставляет :) )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ObjectDelete(object sender, MyCancelEventArgs e)
        {
            Console.WriteLine($"Object {e.ObjectType} was deleted.");
            Console.WriteLine("If you want to undo the deletion, do whatever you want, but your object will still be deleted...");
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
            foreach (var item in _storage.GetStorageObjects<T>())
                if (item is T)
                    yield return item;
        }

        /// <summary>
        /// Обновление свойств объекта
        /// </summary>
        public void ObjectEdit<T>(int id, string properties, T value)
        {
            var obj = GetAllObject<BaseStorageObject>().First(x => x.Id == id);

            Type myType = typeof(BaseStorageObject);
            var checkProp = myType?.GetProperty(properties);

            if (obj != null && checkProp != null)
            {
                if(!checkProp.GetValue(obj).Equals(value))
                checkProp.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Поиск объектов по названию
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Возвращает массив объктов, название которых содержит в себе искомую подстроку</returns>
        public IEnumerable<BaseStorageObject> SearchByTitle(string title)
        {
            var containsList = GetAllObject<BaseStorageObject>()
                .Where(x => x.Title.Contains(title));

            return containsList;
        }

        /// <summary>
        /// Сортирует массив объектов по году издания
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Возвращает отсортированный список, в зависимости от направления сортировки</returns>
        public IEnumerable<BaseStorageObject> SortByYear(bool direction)
        {
            IEnumerable<BaseStorageObject> storage;

            if (direction)
            {
                storage = GetAllObject<BaseStorageObject>().OrderBy(x => x.YearOfPublish.Year);
            }
            else
            {
                storage = GetAllObject<BaseStorageObject>().OrderByDescending(x => x.YearOfPublish.Year);
            }

            return storage;
        }

        /// <summary>
        /// Поиск книги по автору
        /// </summary>
        /// <param name="author"></param>
        /// <returns>Возвращает массив объектов "Book"</returns>
        public IEnumerable<BaseStorageObject> SearchByAuthor(string author)
        {
            var list1 = GetAllObject<Book>().Cast<Book>().Where(x => x.Authors.Contains(author));

            return list1;
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
        /// <returns>Возвращает коллекцию сгруппированных объектов</returns>
        public IDictionary<int, ICollection<BaseStorageObject>> GroupOdjects()
        {
            Dictionary<int, ICollection<BaseStorageObject>> dictionary = new Dictionary<int, ICollection<BaseStorageObject>>();

            foreach (var baseObj in _storage.GetStorageObjects<BaseStorageObject>())
            {
                if (!dictionary.ContainsKey(baseObj.YearOfPublish.Year))
                    dictionary.Add(baseObj.YearOfPublish.Year, new List<BaseStorageObject>());

                dictionary[baseObj.YearOfPublish.Year].Add(baseObj);
            }

            return dictionary;
        }


        /// <summary>
        /// Поиск объектов с указанием произвольных критериев
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <returns>Возвращает коллекцию искомых объектов</returns>
        public IEnumerable<BaseStorageObject> Search<T>(Func<BaseStorageObject,bool> f)
        {
            var containsList = GetAllObject<T>()
                .Where(f);

            return containsList;
        }


        /// <summary>
        /// Сортировка объектов с указанием произвольных критериев
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compare"></param>
        /// <returns></returns>
        public ICollection<BaseStorageObject> Filter<T>(Comparison<BaseStorageObject> compare)
        {
            List<BaseStorageObject> containsList = GetAllObject<T>().ToList();
            containsList.Sort(compare);

            return containsList;
        }
    }
}
