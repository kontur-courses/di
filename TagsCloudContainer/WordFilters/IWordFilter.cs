using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordFilters
{
    interface IWordFilter
    {
        bool IsCorrect(string word);
    }
}
