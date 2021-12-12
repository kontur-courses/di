using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Text
{
    public class ExcludedWordsSelector : ISillyWordSelector
    {
        private HashSet<string> sillyWords;
        public bool IsWordSilly(string word)
        {
            return sillyWords.Contains(word);
        }

        public ExcludedWordsSelector(HashSet<string> sillyWords)
        {
            this.sillyWords = sillyWords;
        }
    }
}
