using System.Collections.Generic;

namespace TagsCloudContainer.TextReader.Parsers
{
    public interface IParser
    {
        public string[] Parse(string text);
    }
}