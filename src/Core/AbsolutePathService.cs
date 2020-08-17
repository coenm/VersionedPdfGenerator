namespace Core
{
    using System;
    using System.IO;

    public class AbsolutePathService : IAbsolutePathService
    {
        public string GetExistingAbsoluteFilename(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return null;

            if (File.Exists(filename))
                return new FileInfo(filename).FullName;

            try
            {
                var fullPath = Path.GetFullPath(filename);
                if (File.Exists(fullPath))
                    return new FileInfo(fullPath).FullName;
            }
            catch (Exception)
            {
                // ignore
            }

            try
            {
                var cd = Directory.GetCurrentDirectory();
                var fullPath = Path.Combine(cd, filename);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            catch (Exception)
            {
                // ignore
            }

            return null;
        }
    }
}