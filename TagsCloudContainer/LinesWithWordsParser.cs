using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class LinesWithWordsParser:ITextParser
    {
        public IEnumerable<string> GetWords(string text)
        {
            return text.Split('\n');
        }
    }
}
