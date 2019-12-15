using System.Collections.Generic;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudForm.WordFilters
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(ICircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words);
    }
}
