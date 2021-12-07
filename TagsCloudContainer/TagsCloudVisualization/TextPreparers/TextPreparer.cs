using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.TextPreparers
{
    public class TextPreparer : ITextPreparer
    {
        private readonly IEnumerable<Func<string, bool>> filters;
        private readonly IEnumerable<Func<string, string>> preparations;

        public TextPreparer(IEnumerable<Func<string, bool>> filters, IEnumerable<Func<string, string>> preparations)
        {
            this.filters = filters;
            this.preparations = preparations;
        }

        public IEnumerable<string> PrepareText(IEnumerable<string> text)
        {
            var prepared = new List<string>();
            var frequencyDict = new Dictionary<string, int>();

            foreach (var word in text)
            {
                if (IsFiltered(word)) continue;

                if (frequencyDict.ContainsKey(word))
                {
                    frequencyDict[word]++;
                }
                else
                {
                    prepared.Add(PrepareWord(word));
                    frequencyDict[word] = 1;
                }
            }

            return prepared.OrderByDescending(x => frequencyDict[x]);
        }

        // Не знаю как лучше... так или чтобы явно отразить, что preparation - Func, через Invoke
        private string PrepareWord(string word) =>
            preparations.Aggregate(word, (current, preparation) => preparation(current));

        private bool IsFiltered(string word) => filters.Any(filter => filter(word));
    }
}