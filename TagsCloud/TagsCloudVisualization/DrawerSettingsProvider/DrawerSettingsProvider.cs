using System;
using TagsCloudVisualization.ColorGenerators;

namespace TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider
{
    public class DrawerSettingsProvider : IDrawerSettingsProvider
    {
        public FontSettings Font { get; init; } = new();
        public IColorGenerator ColorGenerator { get; init; } = new RandomColorGenerator(new Random());
    }
}