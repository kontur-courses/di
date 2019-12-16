using System.Collections.Generic;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextRiders
{
    public interface IFileTextRider
    {
        IEnumerable<Tag> GetTags();
    }
}