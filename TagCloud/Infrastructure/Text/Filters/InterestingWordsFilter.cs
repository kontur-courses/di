using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MyStemWrapper;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class InterestingWordsFilter : IFilter<string>
    {
        private Func<MyStem> myStem;
        public InterestingWordsFilter(Func<MyStem> myStem)
        {
            this.myStem = myStem;
        }
        public IEnumerable<string> Filter(IEnumerable<string> tokens)
        {
            var analyzer = myStem();
            analyzer.Parameters = "-i";
            var analysis = analyzer.Analysis(string.Join(" ", tokens));
            var wordWithTypeRegex = new Regex(@".+\{(?<word>.+)=(?<type>.+)?,.+");
            foreach (Match match in wordWithTypeRegex.Matches(analysis))
            {
                if (match.Success && IsInteresting(match.Groups["type"].Value))
                {
                    yield return match.Groups["word"].Value;
                }
            }
        }

        private bool IsInteresting(string value)
        {
            throw new NotImplementedException();
        }
    }
}