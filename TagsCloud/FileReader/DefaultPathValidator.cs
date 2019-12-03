using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud.FileReader
{
    public class DefaultPathValidator : IPathValidator
    {
        public bool ValidatePath(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }
    }
}
