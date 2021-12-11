using System.Collections.Generic;

namespace App.Infrastructure.Words.Filters
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}