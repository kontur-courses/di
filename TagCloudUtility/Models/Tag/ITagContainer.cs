using System.Drawing;

namespace TagCloud.Utility.Models.Tag
{
    public interface ITagContainer
    {
        void Add(string name, FrequencyGroup frequencyGroup, Size size);
        void Remove(string groupName);
    }
}