namespace Core.Config
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class InputFileLocator : IDynamicConfigFileLocator
    {
        private readonly string[] _defaultFilename;

        public InputFileLocator(params string[] defaultFilename)
        {
            _defaultFilename = defaultFilename;
        }

        public IEnumerable<string> Locate(string inputFilename)
        {
            var result = new List<string>();

            try
            {
                var dir = new FileInfo(inputFilename).Directory.FullName;

                foreach (var filename in _defaultFilename)
                {
                    result.Add(Path.Combine(dir, filename));
                }
            }
            catch (Exception)
            {
                // ignore
            }

            return result;
        }
    }
}