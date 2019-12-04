using System;
using System.Collections.Generic;

namespace TagsCloud.WordsFiltering
{
    public interface IFilter
    {
        Func<List<string>, List<string>> FilterFunc { get; }
    }
}
