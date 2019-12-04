using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Algorithm.Organizing
{
    public interface IWordOrganizer
    {
        IOrderedEnumerable<Word> GetSortedWords(IEnumerable<Word> words);
    }
}