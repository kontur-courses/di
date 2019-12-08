using System.Collections.Generic;

namespace TagsCloudVisualization.Filters
{
    public interface  IFilter
    {
        (bool isValid, string value) Filter(string stemmedString);
        IEnumerable<string> GetFilteredValues(string valueToFilter);
    }
}
