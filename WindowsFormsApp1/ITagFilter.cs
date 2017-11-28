using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public interface ITagFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> tags);
    }
}