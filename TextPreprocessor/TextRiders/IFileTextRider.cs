using System.Collections.Generic;
using System.Data;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextRiders
{
    public interface IFileTextRider
    {
        IEnumerable<Tag> GetTags();
    }
}