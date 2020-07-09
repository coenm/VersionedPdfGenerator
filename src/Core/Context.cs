namespace Core
{
    using System;
    using System.IO;

    public readonly struct Context
    {
        public Context(DateTime now, string filename)
        {
            Now = now;
            FileInfo = new FileInfo(filename);
        }

        public DateTime Now { get; }

        public FileInfo FileInfo { get; }
    }
}