using System.IO;

namespace TagsCloud.TextWorkers
{
    public static class TextReader
    {
        public static string ReadFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
