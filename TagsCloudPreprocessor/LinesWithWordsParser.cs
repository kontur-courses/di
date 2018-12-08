using System.Collections.Generic;

namespace TagsCloudPreprocessor
{
    public class LinesWithWordsParser:ITextParser
    {
        public IEnumerable<string> GetWords(string text)
        {
            return text.Split('\n');
        }
    }
}
