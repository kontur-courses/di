using System.Collections.Generic;


namespace TagsCloudContainer
{
    public interface IWordStorage
    {
        void Add(string word);
        void Add(IEnumerable<string> words);
        List<Word> ToList();
    }
}