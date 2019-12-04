using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.WordsFiltering
{
    public class WasteWordsFilter : IFilter
    {
        public bool IsActive { get; set; } = true;

        public Func<List<string>, List<string>> FilterFunc =>
            collection => IsActive ? collection.Where(w => !IsWasteWord(w)).ToList() : new List<string>(collection);

        private bool IsWasteWord(string word) => word.Length < 2;
    }
}
