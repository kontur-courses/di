using System.Collections.Generic;
using Xceed.Words.NET;

namespace TagsCloudContainer.Data.Readers
{
    public class DocWordsFileReader : IFileFormatReader
    {
        public IEnumerable<string> Extensions { get; } = new[] {".doc", ".docx"};
        public IEnumerable<string> ReadAllWords(string path) => DocX.Load(path).Text.Split();
    }
}