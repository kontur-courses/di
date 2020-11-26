using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public class SimpleTextAnalyzer : BaseFrequenciesTextAnalyzer
    {
        private readonly int minWordLength;
        private readonly char[] separators;

        public SimpleTextAnalyzer(int minWordLength, char[] separators)
        {
            this.minWordLength = minWordLength;
            this.separators = separators;
        }

        protected override IEnumerable<string> EnumerateWordsFrom(string text) =>
            text.Split(separators)
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(w => w.Length >= minWordLength)
                .Select(w => w.ToLower());
    }
}