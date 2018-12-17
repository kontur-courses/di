using System.Collections.Generic;


namespace TagsCloudContainer
{
    public interface IWordStorage
    {
        void Add(string word);
        void AddRange(IEnumerable<string> words);
        List<Word> ToList();
    }
}