using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.WordFilters
{
    interface IWordFilter
    {
        public bool IsValid(string word);
    }
}
