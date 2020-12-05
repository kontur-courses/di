using System.IO;

namespace TagsCloudVisualization.TextProcessing.TextReader
{
    public class TextReader
    {
        public static string ReadAllText(string path)
        {
            if (!File.Exists(path))
                throw new IOException("File you specified does not exist");
            
            return File.ReadAllText(path);
        }
    }
}