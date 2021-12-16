using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Text
{
    public class ExcludedWordsSelector : ISillyWordSelector
    {
        private HashSet<string> excluded;
        public bool IsWordSilly(string word)
        {
            return excluded.Contains(word);
        }

        public ExcludedWordsSelector(IFileReader fileReader, IWordReader wordReader, ExcludedWordsPath path)
        {
            excluded = wordReader.GetUniqueLowercaseWords(fileReader.ReadFile(path.Path)).ToHashSet();
        }
    }
}
