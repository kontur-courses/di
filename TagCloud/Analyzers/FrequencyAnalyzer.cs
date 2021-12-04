using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Analyzers
{
    public class FrequencyAnalyzer : IFrequencyAnalyzer
    {
        public Dictionary<string, int> Analyze(IEnumerable<string> words)
        {
            return words.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
