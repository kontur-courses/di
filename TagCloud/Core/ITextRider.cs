using System.Collections.Generic;

namespace TagCloud
{
    public interface ITextRider
    {
        IEnumerable<Tag> GetWords();
    }
}