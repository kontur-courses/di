using System.Collections.Generic;

namespace TagsCloudContainer.TextParsing.CloudParsing
{
    public interface ICloudWordsParser
    {
        IEnumerable<CloudWord> ParseFrom(IFileWordsParser fileWordsParser, string path);
    }
}