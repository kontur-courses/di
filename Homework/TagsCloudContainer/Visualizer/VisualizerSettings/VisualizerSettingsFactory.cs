﻿using System.Drawing;
using TagsCloudContainer.Visualizer.ColorGenerators;

namespace TagsCloudContainer.Visualizer.VisualizerSettings
{
    public class VisualizerSettingsFactory : IFactory<IVisualizerSettings>
    {
        private ITagCloudSettings settings;
        private readonly IColorGeneratorsResolver resolver;

        public VisualizerSettingsFactory(IColorGeneratorsResolver resolver, ITagCloudSettings settings)
        {
            this.resolver = resolver;
            this.settings = settings;
        }
        
        public IVisualizerSettings Create()
        {
            var colorAlgorithm = resolver.Get(settings.ColoringAlgorythm);
            var backgroundColor = Color.FromName(settings.BackgroundColor);
            var fontFamily = new FontFamily(settings.FontName);
            var font = new Font(fontFamily, settings.FontSize);
            var imageSize = new Size(settings.ImageWidth, settings.ImageHeight);
            return new VisualizerSettings(colorAlgorithm, backgroundColor, font, imageSize);
        }
    }
}