using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloud.TextAnalyze.Extractors
{
    public interface IWordExtractor
    {
        IEnumerable<string> GetWords(string text);
    }
}
