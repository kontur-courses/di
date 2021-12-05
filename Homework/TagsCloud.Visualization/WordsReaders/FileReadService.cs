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

            var fileExtension = Path.GetExtension(fileName)?.Replace(".", "");
            
            if (fileExtension == null)
                throw new ArgumentException($"Unknown extension of file: {fileName}");
                
            var reader = fileReaders.FirstOrDefault(x => x.Extension == fileExtension);

            if (reader == null)
                throw new ArgumentException($"Unsupported file extension: {fileExtension}");

            if (!File.Exists(fileName))
                throw new ArgumentException($"File {fileName} doesn't exists");

            return reader.Read(fileName);
        }
    }
}