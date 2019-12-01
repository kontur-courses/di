using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordFilters
{
    class SimpleWordFilter : IWordFilter
    {
        public bool IsCorrect(string word)
        {
            return true;
        }
    }
}
