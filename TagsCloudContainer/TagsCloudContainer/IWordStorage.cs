using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer
{
    public interface IWordStorage
    {
        void Add(string word);
        void AddRange(IEnumerable<string> words);
        IOrderedEnumerable<Word> ToIOrderedEnumerable();
    }
}