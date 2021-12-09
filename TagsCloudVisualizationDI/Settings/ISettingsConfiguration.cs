using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter;
using TagsCloudVisualizationDI.Layouter.Normalizer;
using TagsCloudVisualizationDI.TextAnalization;
using TagsCloudVisualizationDI.TextAnalization.Analyzer;
using TagsCloudVisualizationDI.TextAnalization.NormalizationMaker;
using TagsCloudVisualizationDI.Visualization;

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

        public string ImagePath
        {
            get => "C:/GitHub/di/TagsCloudVisualizationDI/img_words.jpeg";
        }

        public Func<List<RectangleWithWord>, IVisualization> Visualizator
        {
            get
            {
                return elementsForVisualization
                    => new DefaultVisualization(elementsForVisualization, ImagePath);
                /*
                        elementsForVisualization, new Pen(Color.White, 10),
                        new SolidBrush(Color.White), new Font("Times", 15), ImageFormat.Jpeg, ImagePath,
                        new Size(5000, 5000));
                */
            }
            //get => new DefaultVisualizatorMaker();
            //get => new Func<RectangleWithWord, IVisualization>((List<RectangleWithWord> elementsForVisualisation) => new DefaultVisualization(elementsForVisualisation, new Pen(Color.White, 10),
                //new SolidBrush(Color.White), new Font("Times", 15), ImageFormat.Jpeg, ImagePath, new Size(5000, 5000)));
        }

        public IWordNormalizer Normalization
        {
            get => new WordNormalizerOrigin();
        }

        public IAnalyzer Analyzer
        {
            //var excludedTypes = new []{PartsOfSpeech.SpeechPart}
            get
            {
                /*
                var b = Enum.GetValues(typeof(PartsOfSpeech.SpeechPart))
                    .Cast<PartsOfSpeech.SpeechPart>();
                */

                var excludedTypes = new[]
                {
                    PartsOfSpeech.SpeechPart.CONJ, PartsOfSpeech.SpeechPart.INTJ,
                    PartsOfSpeech.SpeechPart.PART, PartsOfSpeech.SpeechPart.PR,
                };
                return  new DefaultAnalyzer(excludedTypes);
            }
    }

        public IContentFiller Filler
        {
            get => new CircularCloudLayouterForRectanglesWithText(LayouterCenter, Visualizator);
        }
        
    }
}
