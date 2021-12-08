using System;
using System.IO;
using System.Text;

namespace TagsCloudVisualization.FileReader
{
    public class TextFileReader : ITextFileReader
    {
        public string[] ReadText(string path, Encoding encoding)
        {
            if (FileIsExist(path))
            {
                return File.ReadAllLines(path, encoding);
            }
            throw new Exception($"Giving path {path} is not valid");
        }

        public string[] ReadText(string path)
        {
            return ReadText(path, Encoding.UTF8);
        }

        private bool FileIsExist(string path)
        {
            return true;
        }
    }
}
