using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudContainer
{
    class FileReaderTxt : IFileReader
    {
        public string Format { get; set; }

        public FileReaderTxt()
        {
            Format = "txt";
        }

        public IEnumerable<string> ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
