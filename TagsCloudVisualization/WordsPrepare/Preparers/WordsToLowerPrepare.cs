using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPrepare.Preparers
{
    public class WordsToLowerPrepare : IWordsPreparer
    {
        public IEnumerable<string> Prepare(IEnumerable<string> words) =>
            words.Select(word => word.ToLower());
    }
}