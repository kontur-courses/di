using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudVisualization.WordsProvider.FileReader;

namespace TagsCloudVisualization.WordsProvider
{
    internal class FileReadService : IFileReadService
    {
        private const string WordsSplitPattern = @"\W+";
        private readonly Regex wordsSplit = new Regex(WordsSplitPattern);
        private readonly string path;
        private readonly IEnumerable<IWordsReader> readers;
        private readonly string extension;

        public FileReadService(string path, IEnumerable<IWordsReader> readers)
        {
            this.path = path;
            CheckFileExistsAndThrowArgumentExceptionIfNot();
            this.readers = readers;
            extension = Path.GetExtension(path);
        }

        public IEnumerable<string> GetFileContent()
        {
            var reader = readers.FirstOrDefault(reader => reader.IsSupportedFileExtension(extension));
            if (reader == null)
                throw new ArgumentException($"Unsupported file extension: {extension}");
            CheckFileExistsAndThrowArgumentExceptionIfNot();
            var text = reader.GetFileContent(path);
            return wordsSplit.Split(text);
        }

        private void CheckFileExistsAndThrowArgumentExceptionIfNot()
        {
            if (!File.Exists(path))
                throw new ArgumentException($"File {path} doesn't exists");
        }
    }
}