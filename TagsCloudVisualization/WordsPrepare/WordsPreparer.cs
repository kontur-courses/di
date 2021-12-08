using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPrepare
{
    public class WordsPreparer : IWordsPreparer
    {
        private readonly IEnumerable<IWordsPreparer> preparers;

        public WordsPreparer(IEnumerable<IWordsPreparer> preparers) => this.preparers = preparers;

        public IEnumerable<string> Prepare(IEnumerable<string> words)
        {
            return preparers
                .Aggregate(words, (current, preparer) => preparer.Prepare(current));
        }
    }
}