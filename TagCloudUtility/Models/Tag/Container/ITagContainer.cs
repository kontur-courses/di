using System.Collections.Generic;

namespace TagCloud.Utility.Models.Tag.Container
{
    public interface ITagContainer: IEnumerable<(string, ITagGroup)>
    {
        void Add(string name, FrequencyGroup frequencyGroup, int fontSize);
        void Remove(string groupName);

        ITagGroup GetTagGroupFor(double frequencyCoef);
    }
}