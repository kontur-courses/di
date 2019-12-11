using System.Collections.Generic;

namespace TagCloud.TextProvider
{
    public interface ITextProvider
    {
        List<string> GetAllLines();
        List<string> GetAllLines(IEnumerable<string> paths);
    }
}