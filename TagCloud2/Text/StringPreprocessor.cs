using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class StringPreprocessor : IStringPreprocessor
    {
        ISillyWordRemover sillyRemover;
        ISillyWordSelector sillySelector;
        public string PreprocessString(string input)
        {
            return sillyRemover.RemoveSillyWords(input, sillySelector);
        }

        public StringPreprocessor(ISillyWordRemover remover, ISillyWordSelector selector)
        {
            sillySelector = selector;
            sillyRemover = remover;
        }
    }
}
