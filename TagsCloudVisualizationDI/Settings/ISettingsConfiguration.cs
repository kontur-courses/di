using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter;
using TagsCloudVisualizationDI.Layouter.Normalizer;
using TagsCloudVisualizationDI.TextAnalization;
using TagsCloudVisualizationDI.TextAnalization.Analyzer;
using TagsCloudVisualizationDI.TextAnalization.NormalizationMaker;
using TagsCloudVisualizationDI.TextAnalization.VisualizatorMaker;

namespace TagsCloudVisualizationDI.Settings
{
    public interface ISettingsConfiguration
    {
        public Size ElementSize
        {
            get => new Size(100, 100);
        }

        public Point LayouterCenter
        {
            get => new Point(2500, 2500);
        }

        public ITextFileReader FileReader
        {
            get => new DefaultTextFileReader();
        }

        public IVisualizatorMaker Visualizator
        {
            get => new DefaultVisualizatorMaker();
        }

        public IWordNormalizer Normalization
        {
            get => new WordNormalizerOrigin();
        }

        public IAnalyzer Analyzer
        {
            get => 
                new DefaultAnalyzer(Enum.GetValues(typeof(PartsOfSpeech.SpeechPart))
                    .Cast<PartsOfSpeech.SpeechPart>());
        }

        public IContentFiller Filler
        {
            get => new CircularCloudLayouterForRectanglesWithText(LayouterCenter);
        }
    }
}
