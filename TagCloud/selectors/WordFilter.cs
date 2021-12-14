using System.Collections.Generic;
using System.Linq;

namespace TagCloud.selectors
{
    public class WordFilter : IFilter<string>
    {
        private readonly List<IChecker<string>> checkers;

        public WordFilter(List<IChecker<string>> checkers)
        {
            this.checkers = checkers;
        }

        public IEnumerable<string> Filter(IEnumerable<string> source) =>
            checkers.Count == 0
                ? source
                : source.Where(word => checkers.Any(checker => checker.IsValid(word)));
    }
}