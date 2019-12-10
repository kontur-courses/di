using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudForm.WordFilters
{
    interface IWordsFilter
    {
        IEnumerable<string> Filter(CircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words);
    }
}
