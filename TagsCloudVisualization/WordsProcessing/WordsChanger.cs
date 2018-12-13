using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProcessing
{
    public class WordsChanger : IWordsChanger
    {
        public IEnumerable<string> ChangeWords(IEnumerable<string> words)
        {
            return words.Select(w => w.ToLower());
        }
    }
}