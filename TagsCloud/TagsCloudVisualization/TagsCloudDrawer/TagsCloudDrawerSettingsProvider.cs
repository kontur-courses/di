using System;
using System.Drawing;
using TagsCloudVisualization.ColorGenerators;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public class TagsCloudDrawerSettingsProvider : ITagsCloudDrawerSettingsProvider
    {
        public FontFamily FontFamily { get; init; } = FontFamily.GenericSansSerif;
        public IColorGenerator ColorGenerator { get; init; } = new RandomColorGenerator(new Random());
    }
}