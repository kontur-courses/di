using System;
using TagsCloudDrawer.ColorGenerators;

namespace TagsCloudVisualization.DrawerSettingsProvider
{
    public class DrawerSettingsProvider : IDrawerSettingsProvider
    {
        public FontSettings Font { get; init; } = new();
        public IColorGenerator ColorGenerator { get; init; } = new RandomColorGenerator(new Random());
    }
}