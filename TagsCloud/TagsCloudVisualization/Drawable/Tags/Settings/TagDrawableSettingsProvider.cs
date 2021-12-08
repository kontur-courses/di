using System;
using TagsCloudDrawer.ColorGenerators;
using TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator;

namespace TagsCloudVisualization.Drawable.Tags.Settings
{
    public class TagDrawableSettingsProvider : ITagDrawableSettingsProvider
    {
        public FontSettings Font { get; init; } = new();

        public ITagColorGenerator ColorGenerator { get; init; } =
            new RandomTagColorGenerator(new RandomColorGenerator(new Random()));
    }
}