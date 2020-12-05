using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer
{
    class FileReader : IFileReader
    {
        public string Format { get; set; }
        private IFileReader[] fileReaders;

        public FileReader(params IFileReader[] fileReaders)
        {
            this.fileReaders = fileReaders;
            Format = null;
        }

        public IEnumerable<string> ReadAllLines(string filePath)
        {
            var fileFormat = GetFormat(filePath);
            foreach (var fileReader in fileReaders)
            {
                if (fileReader.Format == fileFormat)
                    return fileReader.ReadAllLines(filePath);
            }

            throw new ArgumentException();
        }

        private string GetFormat(string filePath)
        {
            var format = new StringBuilder();
            for (var i = filePath.Length - 1; i >= 0; i--)
            {
                if (filePath[i] == '.')
                    return format.ToString();
                format.Insert(0, filePath[i]);
            }
            throw new ArgumentException();
        }
    }
}
