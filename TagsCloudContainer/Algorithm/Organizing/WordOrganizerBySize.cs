using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Algorithm.Organizing
{
    public class WordOrganizerBySize : IWordOrganizer
    {
        public IOrderedEnumerable<Word> GetSortedWords(IEnumerable<Word> words)
        {
            return words.OrderBy(w => w.Size);
        }
    }
}