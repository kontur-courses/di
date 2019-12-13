using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.WordsFiltering
{
    public class UpperCaseFilter : IFilter
    {
        public Func<List<string>, List<string>> FilterFunc =>
            words => words.Select(w => w.ToUpper()).ToList();
    }
}
