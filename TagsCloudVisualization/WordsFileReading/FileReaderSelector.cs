using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.WordsFileReading
{
    public class FileReaderSelector
    {
        private readonly IDictionary<string, IFileReader> fileReaderByExtension;

        public FileReaderSelector(IEnumerable<IFileReader> fileReaders)
        {
            fileReaderByExtension = new Dictionary<string, IFileReader>();

            foreach (var reader in fileReaders)
                foreach (var extension in reader.SupportedTypes())
                    fileReaderByExtension[extension] = reader;
        }

        public IFileReader SelectFileReader(string fileName)
        {
            var extension = fileName.ExtractFileExtension();
            if (extension != null && fileReaderByExtension.ContainsKey(extension))
                return fileReaderByExtension[extension];

            throw new ArgumentException("Input file is not supported");
        }
    }
}
