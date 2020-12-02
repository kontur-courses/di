using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class FileReaderProvider
    {
        private readonly IEnumerable<IFileAllLinesReader> fileReaders;

        public FileReaderProvider(IEnumerable<IFileAllLinesReader> fileReaders)
        {
            this.fileReaders = fileReaders;
        }

        public IFileAllLinesReader GetFileReader(string extension)
        {
            return fileReaders.FirstOrDefault(x => x.Extensions.Contains(extension));
        }
    }
}