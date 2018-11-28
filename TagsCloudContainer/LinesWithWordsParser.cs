using System.Collections.Generic;

namespace TagsCloudContainer
{
    class LinesWithWordsParser:ITextParser
    {
        public IEnumerable<string> GetWords(string text)
        {
            return text.Split('\n');
        }
    }
}
