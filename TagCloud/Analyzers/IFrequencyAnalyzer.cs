using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Analyzers
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, int> Analyze(IEnumerable<string> words);
    }
}
