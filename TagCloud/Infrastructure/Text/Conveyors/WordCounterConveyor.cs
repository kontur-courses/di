using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Conveyors
{
    public class WordCounterConveyor : IConveyor<string>
    {
        public IEnumerable<(string token, TokenInfo info)> Handle(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            tokens = tokens.ToList();
            var counts = GetCount(tokens.Select(pair => pair.token));
            foreach (var word in counts.Keys)
            {
                var (_, info) = tokens.First(pair => pair.token == word);
                info.Frequency = counts[word];
                yield return (word, info);
            }
        }

        private Dictionary<string, int> GetCount(IEnumerable<string> tokens)
        {
            var count = new Dictionary<string, int>();
            foreach (var token in tokens)
                if (count.ContainsKey(token))
                    count[token]++;
                else
                    count.Add(token, 1);

            return count;
        }
    }
}