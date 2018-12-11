using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ICloudConfigurator
    {
        IEnumerable<KeyValuePair<string, int>> ConfigureCloud();
    }
}
