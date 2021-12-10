using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ColorMappers
{
    public class StaticWordColorMapper : IWordColorMapper
    {
        private readonly IDefaultColorSettings settings;
        public WordColorMapperType Type => WordColorMapperType.Static;

        public StaticWordColorMapper(IDefaultColorSettings settings)
        {
            this.settings = settings;
        }

        public Dictionary<WordLayout, Color> GetColorMap(CloudLayout layout) =>
            layout.WordLayouts.ToDictionary(wordLayout => wordLayout, _ => settings.Color);
    }
}