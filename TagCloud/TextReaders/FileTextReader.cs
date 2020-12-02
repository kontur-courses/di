using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Settings;

namespace TagCloud.TextReaders
{
    public class FileTextReader : ITextReader
    {
        private readonly string filePath;
        
        public FileTextReader(FileTextReaderSettings settings)
        {
            filePath = settings.FilePath;
        }
        
        public List<string> ReadWords()
        {
            return File.ReadLines(filePath).ToList();
        }
    }
}