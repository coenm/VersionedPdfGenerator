namespace Core.Config
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class UserDataFileLocator : IDynamicConfigFileLocator
    {
        private readonly string[] _defaultFilename;

        public UserDataFileLocator(params string[] defaultFilename)
        {
            _defaultFilename = defaultFilename;
        }

        public IEnumerable<string> Locate(string inputFilename)
        {
            try
            {
                var userDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                return _defaultFilename
                       .Select(filename => Path.Combine(userDir, filename))
                       .ToList();
            }
            catch (Exception)
            {
              // ignore
            }

            return new List<string>(0);
        }
    }
}