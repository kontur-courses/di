using System.Collections.Generic;
using TagsCloudContainer.Layouter.PointsProviders;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;

namespace TagsCloud.Console
{
    public interface IAppSettings
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int FontSize { get; set; }
        public int MinWordLength { get; set; }
        public string BackgroundColor { get; set; }
        public ICollection<SpeechPart> SelectedSpeechParts { get; set; }
        public LayoutAlrogorithm LayoutAlgorythm { get; set; }
        public PalleteType ColoringAlgorythm { get; set; }
        public string FontName { get; set; }
    }
}