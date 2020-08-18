namespace Core
{
    using System;
    using System.IO;

    public readonly struct Context
    {
        public Context(DateTime now, string filename, DefaultFormats defaultDateFormats)
        {
            Now = now;
            DefaultDateFormats = defaultDateFormats;
            FileInfo = new FileInfo(filename);
        }

        public DateTime Now { get; }

        public FileInfo FileInfo { get; }

        public DefaultFormats DefaultDateFormats { get; }
    }
}