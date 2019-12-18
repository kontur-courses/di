using System.Collections.Generic;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextRiders
{
    public interface IFileTextRider
    {
        TextRiderConfig RiderConfig { get; }
        IEnumerable<Tag> GetTags();
        bool CanReadFile();
    }
}