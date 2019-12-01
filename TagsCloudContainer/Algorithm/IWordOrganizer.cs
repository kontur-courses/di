using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Algorithm
{
    public interface IWordOrganizer
    {
        IOrderedEnumerable<Word> GetSortedWords(IEnumerable<Word> words);
    }
}