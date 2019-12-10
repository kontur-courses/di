using System.Collections.Generic;
using System.Text;

namespace TagCloud.TextProvider
{
    public interface ITextProvider
    {
        List<string> GetAllLines();
        List<string> GetAllLines(IEnumerable<string> paths);
        Encoding TextEncoding { get; set; }
    }
}