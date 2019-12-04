using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.WordsFiltering
{
    public class LowerCaseFilter : IFilter
    {
        public bool IsActive { get; set; } = true;

        public Func<List<string>, List<string>> FilterFunc =>
            collection => IsActive ? collection.Select(w => w.ToLower()).ToList() : new List<string>(collection);
    }
}
