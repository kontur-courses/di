using System.Collections.Generic;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudForm.WordFilters
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(CircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words);
    }
}
