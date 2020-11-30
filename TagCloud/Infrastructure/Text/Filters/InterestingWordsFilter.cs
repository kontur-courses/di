using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MyStemWrapper;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class InterestingWordsFilter : IFilter<string>
    {
        private readonly string myStemPath;
        private readonly Func<IExcludeTypesSettingsProvider> excludeTypesSettingsProvider;

        public InterestingWordsFilter(string myStemPath, Func<IExcludeTypesSettingsProvider> excludeTypesSettingsProvider)
        {
            this.myStemPath = myStemPath;
            this.excludeTypesSettingsProvider = excludeTypesSettingsProvider;
        }
        public IEnumerable<string> Filter(IEnumerable<string> tokens)
        {
            var analyzer = new MyStem() {PathToMyStem = myStemPath, Parameters = "-i"};
            var analysis = analyzer.Analysis(string.Join(" ", tokens));
            var wordWithTypeRegex = new Regex(@".+\{(?<word>.+?)=(?<type>.+?),.+}");
            foreach (Match match in wordWithTypeRegex.Matches(analysis))
            {
                if (match.Success && IsInteresting(match.Groups["type"].Value))
                {
                    yield return match.Groups["word"].Value;
                }
            }
        }

        private bool IsInteresting(string type) => !excludeTypesSettingsProvider().ExcludedTypes.Contains(type);
    }
}