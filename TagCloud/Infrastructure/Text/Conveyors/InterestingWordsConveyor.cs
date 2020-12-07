using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public class InterestingWordsConveyor : IConveyor<string>
    {
        private readonly Func<IExcludeTypesSettingsProvider> excludeTypesSettingsProvider;

        public InterestingWordsConveyor(Func<IExcludeTypesSettingsProvider> excludeTypesSettingsProvider)
        {
            this.excludeTypesSettingsProvider = excludeTypesSettingsProvider;
        }

        public IEnumerable<(string token, TokenInfo info)> Handle(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            return tokens.Where(pair => IsInteresting(pair.info.WordType));
        }

        private bool IsInteresting(WordType type)
        {
            return !excludeTypesSettingsProvider().ExcludedTypes.Contains(type);
        }
    }
}