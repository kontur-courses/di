using System.Collections.Generic;

namespace TagsCloudPreprocessor
{
    public interface IWordExcluder
    {
        HashSet<string> GetExcludedWords();
        void SetExcludedWord(string word);
    }
}
