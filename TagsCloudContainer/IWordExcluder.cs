using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordExcluder
    {
        HashSet<string> GetExcludedWords();
        void SetExcludedWord(string word);
    }
}
