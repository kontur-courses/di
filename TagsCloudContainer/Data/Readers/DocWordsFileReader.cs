using System.Collections.Generic;
using System.Linq;
using Xceed.Words.NET;

namespace TagsCloudContainer.Data.Readers
{
    public class DocWordsFileReader : IFileFormatReader
    {
        public IEnumerable<string> Extensions { get; } = new[] {".doc", ".docx"};

        public IEnumerable<string> ReadAllWords(string path)
        {
            return DocX.Load(path).Paragraphs.Select(p => p.Text);
        }
    }
}