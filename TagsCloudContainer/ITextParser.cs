using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ITextParser
    {
        public Dictionary<string, int> GetParsedText(string text);
    }
}