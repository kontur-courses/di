using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudVisualization.WordsProvider.FileReader;

namespace TagsCloudVisualization.WordsProvider
{
    public class FileReadService : IFileReadService
    {
        private readonly string path;
        private readonly IEnumerable<IWordsReader> readers;
        private const string WordsSplitPattern = @"\W+";

        public FileReadService(string path, IEnumerable<IWordsReader> readers)
        {
            this.path = path;
            if (!File.Exists(path))
                throw new ArgumentException($"File {path} doesn't exists");
            this.readers = readers;
        }
        
        public IEnumerable<string> Read()
        {
            var extension = Path.GetExtension(path);
            var reader = readers.FirstOrDefault(reader => reader.CanRead(extension));
            if (reader == null)
                throw new ArgumentException($"Unsupported file extension: {extension}");
            var text = reader.Read(path);
            return Regex.Split(text, WordsSplitPattern);
        }
    }
}