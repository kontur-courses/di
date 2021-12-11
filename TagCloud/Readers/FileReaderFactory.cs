using System.Collections.Generic;

namespace TagCloud.Readers
{
    public class FileReaderFactory : IFileReaderFactory
    {
        private static readonly Dictionary<string, IFileReader> fileReadersFactory =
            new()
            {
                {".txt", new TextReader()}
            };
        
        public IFileReader Create(string fileExtension)
        {
            return fileReadersFactory[fileExtension];
        }
    }
}
