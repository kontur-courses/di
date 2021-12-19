using System.Collections.Generic;
using System.Linq;

namespace TagCloud2.Text
{
    public class ExcludedWordsSelector : ISillyWordSelector
    {
        private readonly HashSet<string> excluded;
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
