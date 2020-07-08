namespace Core.Config
{
    using System.Collections.Generic;

    public interface IDynamicConfigFileLocator
    {
        IEnumerable<string> Locate(string inputFilename);
    }


}