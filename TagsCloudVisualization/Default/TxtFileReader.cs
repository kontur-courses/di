using System.IO;
using System.Linq;

namespace TagsCloudVisualization.Default
{
    public class TxtFileReader : IFileReader
    {
        public bool CanReadFile(FileInfo file)
        {
            if (!file.Exists)
                return false;
            var extension = Path.GetExtension(file.Name);
            return extension == "txt";

        }

        public string ReadFile(FileInfo file)
        {
            return file.OpenText().ReadToEnd();
        }
    }
}