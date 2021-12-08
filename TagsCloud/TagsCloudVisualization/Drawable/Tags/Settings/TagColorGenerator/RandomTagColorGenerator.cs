using System.Drawing;
using TagsCloudDrawer.ColorGenerators;

namespace TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator
{
    public class RandomTagColorGenerator : ITagColorGenerator
    {
        private readonly IColorGenerator _generator;

        public RandomTagColorGenerator(IColorGenerator generator)
        {
            _generator = generator;
        }

        public Color Generate(Tag tag) => _generator.Generate();
    }
}