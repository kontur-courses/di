using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextAnalization.Analyzer
{
    public interface IAnalyzer
    {
        IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words);
        //bool CheckWord(string word);
    }
}
