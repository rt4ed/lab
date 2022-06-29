using System;

namespace LibraryNetwork.EventHandler
{
    public class BaseStorageObjectArgs : EventArgs
    {
        public BaseStorageObjectArgs(Type type)
        {
            ObjectType = type;
        }

        public Type ObjectType { get; set; }
    }
}
