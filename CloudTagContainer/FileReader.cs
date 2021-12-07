using System;
using System.IO;

namespace CloudTagContainer
{
    public class FileReader: IFileReader
    {
        private string path = String.Empty;
        
        public void SetPath(string path)
        {
            this.path = path;
        }

        public string[] ReadWords()
        {
            using var streamReader = new StreamReader(path);
            //FIXME
            var allFile = streamReader.ReadToEnd();
            return ParseOnWords(allFile);
        }

        private string[] ParseOnWords(string str)
        {
            return str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}