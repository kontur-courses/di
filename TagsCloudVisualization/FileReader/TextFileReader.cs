using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.FileReader
{
    public class TextFileReader : ITextFileReader
    {
        public string[] ReadText(string path, Encoding encoding)
        {
            if (PathIsValid(path))
            {
                return File.ReadAllLines(path, encoding);
            }
            throw new Exception($"Giving path {path} is not valid");
        }

        public string[] ReadText(string path)
        {
            return ReadText(path, Encoding.UTF8);
        }

        private bool PathIsValid(string path)
        {
            return true;
        }
    }
}
