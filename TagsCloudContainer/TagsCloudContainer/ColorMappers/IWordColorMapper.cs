using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.DependencyInjection;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.ColorMappers
{
    public interface IWordColorMapper : IService<WordColorMapperType>
    {
        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout);
    }
}