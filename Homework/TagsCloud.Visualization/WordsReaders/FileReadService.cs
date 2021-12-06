using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloud.Visualization.WordsReaders.FileReaders;

namespace TagsCloud.Visualization.WordsReaders
{
    public class FileReadService : IWordsReadService
    {
        private readonly string fileName;
        private readonly IEnumerable<IFileReader> fileReaders;

        public FileReadService(string fileName, IEnumerable<IFileReader> fileReaders)
        {
            this.fileName = fileName;
            this.fileReaders = fileReaders;
        }

        public string Read()
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {fileName} doesn't exists");

            var fileExtension = Path.GetExtension(fileName)?.Replace(".", "");

            if (fileExtension == null)
                throw new ArgumentException($"Unknown extension of file: {fileName}");

            var reader = fileReaders.FirstOrDefault(x => x.CanRead(fileExtension));

            if (reader == null)
                throw new ArgumentException($"Unsupported file extension: {fileExtension}");

            return reader.Read(fileName);
        }
    }
}