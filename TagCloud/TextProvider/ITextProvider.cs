using System.Collections.Generic;
using System.Text;

namespace TagCloud.TextProvider
{
    public interface ITextProvider
    {
        List<string> GetAllWords();
        List<string> GetAllWords(IEnumerable<string> paths);
        Encoding TextEncoding { get; set; }
    }
}