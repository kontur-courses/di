using System.IO;

namespace TagsCloudVisualisation.InputStream.FileInputStream
{
    public class TxtEncoder : IFileEncoder
    {
        public string GetText(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}