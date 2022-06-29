namespace Epam.Rd.Application.Interfaces
{
    public interface IStorage
    {
        //Создал для класса FileStorage отдельный интерфейс, т.к. метод Save может иметь разную реализацию в разных местах программы, тем самым решив проблему DIP
        public void Save<T>(T obj);
    }
}
