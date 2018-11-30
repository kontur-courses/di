using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class TextParser:ITextParser
    {
        public IEnumerable<string> GetWords(string text)
        {
            var separators = new[]{' ', '\n'};

            return text.Split(separators);
        }
    }
}
