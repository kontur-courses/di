using DeepMorphy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.TextProcessors
{
    public class LemmatizationProcessor : IWordProcessor
    {
        private readonly MorphAnalyzer morph;
        public LemmatizationProcessor()
        {
            morph = new MorphAnalyzer(withLemmatization: true, withTrimAndLower: false);
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return morph.Parse(words).Select(morphInf => morphInf.Tags[0].Lemma);
        }
    }
}
