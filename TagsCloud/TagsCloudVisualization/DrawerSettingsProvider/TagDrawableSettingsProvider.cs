using System;
using TagsCloudDrawer.ColorGenerators;
using TagsCloudVisualization.DrawerSettingsProvider.TagColorGenerator;

namespace TagsCloudVisualization.DrawerSettingsProvider
{
    public class TagDrawableSettingsProvider : ITagDrawableSettingsProvider
    {
        public FontSettings Font { get; init; } = new();

        public ITagColorGenerator ColorGenerator { get; init; } =
            new RandomTagColorGenerator(new RandomColorGenerator(new Random()));
    }
}