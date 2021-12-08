using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Rendering
{
    public class StaticWordColorMapper : IWordColorMapper
    {
        private readonly Color color;

        public StaticWordColorMapper(Color color)
        {
            this.color = color;
        }

        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout) =>
            layout.WordLayouts.ToDictionary(wordLayout => wordLayout, _ => color);
    }
}