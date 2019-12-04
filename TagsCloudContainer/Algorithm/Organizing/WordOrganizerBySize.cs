using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.Algorithm.Organizing
{
    public class WordOrganizerBySize : IWordOrganizer
    {
        public IOrderedEnumerable<Word> GetSortedWords(IEnumerable<Word> words)
        {
            return words.OrderByDescending(w => w.Size.GetArea());
        }
    }
}