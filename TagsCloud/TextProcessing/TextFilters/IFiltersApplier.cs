using System;
using System.Collections.Generic;

namespace TagsCloud.TextProcessing.TextFilters
{
    public interface IFiltersApplier
    {
        IEnumerable<string> ApplyFilters(IEnumerable<string> text);
        IEnumerable<string> GetFilerNames();
        IFiltersApplier Register(string filterName, Func<ITextFilter> textFilter);
    }
}
