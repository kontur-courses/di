using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Utility.Models.Tag.Container
{
    public interface ITagContainer: IEnumerable<ITagGroup>
    {
        void Add(string name, FrequencyGroup frequencyGroup, Size size);
        void Remove(string groupName);
    }
}