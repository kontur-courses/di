using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsFileReading
{
    public class FileReaderSelector
    {
        private readonly IEnumerable<IFileReader> fileReaders;

        public FileReaderSelector(IEnumerable<IFileReader> fileReaders)
        {
            this.fileReaders = fileReaders;
        }

        public IFileReader SelectFileReader(string extension)
        {
            foreach (var fileReader in fileReaders)
                if (fileReader.SupportedTypes().Contains(extension))
                    return fileReader;
            
            throw new ArgumentException("Input file extension is not supported");
        }
    }
}
