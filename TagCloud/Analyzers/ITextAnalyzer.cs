using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Analyzers
{
    public interface ITextAnalyzer
    {
        IEnumerable<string> Analyze(string text);
    }
}
