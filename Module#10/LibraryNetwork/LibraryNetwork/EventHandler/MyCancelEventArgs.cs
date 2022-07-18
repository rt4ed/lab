using System;

namespace LibraryNetwork.EventHandler
{
    public class MyCancelEventArgs
    {
        public MyCancelEventArgs(Type type, int id, string title)
        {
            ObjectType = type;
            Id = id;
            Title = title;
        }

        public Type ObjectType { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool Cancel { get; set; }
    }
}
