using System.Collections.Generic;

namespace TagCloud.Readers
{
    public class FileReaderFactory : IFileReaderFactory
    {
        private static readonly Dictionary<string, IFileReader> fileReadersFactory =
            new()
            {
                {"txt", new TextReader()},
                {"xml", new XmlFileReader()},
                {"docx", new DocFileReader()}
            };
        
        public IFileReader Create(string fileExtension)
        {
            return fileReadersFactory[fileExtension];
        }
    }
}
