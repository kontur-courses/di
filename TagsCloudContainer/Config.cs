using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;

namespace TagsCloudContainer
{
    public class Config : ICustomWordsRemoverConfig, IResultRendererConfig, IFormatterConfig, ILayouterConfig
    {
        public Size ImageSize { get; set; }
        public IEnumerable<string> CustomBoringWords { get; set; } = Enumerable.Empty<string>();

        public Font Font { get; set; }

        public Color Color { get; set; }

        public bool FrequentWordsAsHuge { get; set; } = true;

        public float FontMultiplier { get; set; } = 7;

        public PointF CenterPoint { get; set; } = PointF.Empty;

        public double AngleDelta { get; set; } = 10;
    }
}