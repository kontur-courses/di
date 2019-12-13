using System.Collections.Generic;

namespace TagsCloudGenerator.Tools
{
    public interface IWordsParser
    {
        List<string> Parse(string text);
    }
}