using System.Collections.Generic;

namespace TagsCloudContainerCore.InterfacesCore;

public interface IStatisticMaker
{
    void AddTags(IEnumerable<string> tags);
    public KeyValuePair<string, int> GetMinTag();
    public KeyValuePair<string, int> GetMaxTag();
    IEnumerable<KeyValuePair<string, int>> CountedTags { get; }
}