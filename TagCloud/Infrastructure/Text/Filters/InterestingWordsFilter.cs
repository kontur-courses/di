using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class InterestingWordsFilter : IFilter<string>
    {
        private readonly Func<IExcludeTypesSettingsProvider> excludeTypesSettingsProvider;

        public InterestingWordsFilter(Func<IExcludeTypesSettingsProvider> excludeTypesSettingsProvider)
        {
            this.excludeTypesSettingsProvider = excludeTypesSettingsProvider;
        }

        public IEnumerable<(string token, TokenInfo info)> Filter(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            return tokens.Where(pair => IsInteresting(pair.info.WordType));
        }
        
        private bool IsInteresting(WordType type) => !excludeTypesSettingsProvider().ExcludedTypes.Contains(type);
    }
}