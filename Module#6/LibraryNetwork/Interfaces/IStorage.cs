namespace LibraryNetwork.Interfaces
{
    public interface IStorage
    {
        void Add(BaseStorageObject obj);

        void Delete(BaseStorageObject obj);

        BaseStorageObject[] GetStorageObjects();
    }
}
