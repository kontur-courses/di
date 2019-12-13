using System;
using System.Drawing;
using TagsCloudContainer.Core.Generators;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Measurers;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Cloud
{
    public class TagsCloudSettings : ArchimedeanSpiral.ISettings, ProbabilityWordMeasurer.ISettings,
        ConstantColorsPainter.ISettings, TagsCloudVisualizer.ISettings
    {
        public double Distance { get; set; } = 1;
        public double Delta { get; set; } = Math.PI / 180;
        public string WordsPath { get; set; } = "Resources/words.txt";
        public string BoringWordsPath { get; set; } = "Resources/boring_words.txt";
        public FontFamily FontFamily { get; set; } = FontFamily.GenericMonospace;
        public float SizeFactor { get; set; } = 100f;
        public Color TextColor { get; set; } = Color.Black;
        public Color FillColor { get; set; } = Color.Transparent;
        public Color BorderColor { get; set; } = Color.Transparent;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public Size? ImageSize { get; set; } = null;
    }
}