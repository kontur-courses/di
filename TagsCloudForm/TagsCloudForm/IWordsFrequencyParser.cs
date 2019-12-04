using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public interface IWordsFrequencyParser
    {
        Dictionary<string, int> GetWordsFrequency(string[] lines, SpellCheckerFilter filter, LanguageEnum language);
    }
}
