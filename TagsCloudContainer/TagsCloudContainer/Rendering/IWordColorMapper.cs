using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Rendering
{
    public interface IWordColorMapper
    {
        public ColorMapperType Type { get; }
        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout);
    }
}