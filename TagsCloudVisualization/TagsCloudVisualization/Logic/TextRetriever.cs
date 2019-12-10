using System;
using System.IO;
using System.Text;

namespace TagsCloudVisualization.Logic
{
    public static class TextRetriever
    {
        public static string RetrieveTextFromFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException();
            if (!File.Exists(path))
                throw new ArgumentException();
            string text = File.ReadAllText(path, Encoding.UTF8);
            return text;
        }
    }
}