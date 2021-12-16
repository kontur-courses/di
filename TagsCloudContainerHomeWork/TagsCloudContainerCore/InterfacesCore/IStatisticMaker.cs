using System.Collections.Generic;

namespace TagsCloudContainerCore.InterfacesCore;

public interface IStatisticMaker
{
    void AddTagValues(IEnumerable<string> tags);
    public KeyValuePair<string, int> GetMostFrequentTag();
    public KeyValuePair<string, int> GetLeastFrequentTag();
    IEnumerable<KeyValuePair<string, int>> CountedTags { get; }
}