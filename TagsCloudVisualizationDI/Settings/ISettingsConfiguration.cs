using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter;
using TagsCloudVisualizationDI.Layouter.Normalizer;
using TagsCloudVisualizationDI.TextAnalization;
using TagsCloudVisualizationDI.TextAnalization.Analyzer;
using TagsCloudVisualizationDI.Visualization;

namespace TagsCloudVisualizationDI.Settings
{
    public interface ISettingsConfiguration
    {
        public List<string> ExcludedWords => new List<string>();

        public Size ElementSize => new Size(100, 100);

        public Point LayouterCenter => new Point(2500, 2500);

        public string SavePath => "C:/GitHub/di/TagsCloudVisualizationDI/img_words";

        public string FilePath => "C:/GitHub/di/TagsCloudVisualizationDI/War_and_piece.Docx";

        public string MystemPath => "C:/GitHub/di/TagsCloudVisualizationDI/mystem.exe";

        public string Args => "-lndw -ig";

        public Encoding TextEncoding => Encoding.UTF8;

        public ITextFileReader FileReader => new DefaultTextFileReader(FilePath, SavePath, MystemPath, Args, TextEncoding);


        public SolidBrush Brush => new SolidBrush(Color.White);

        public Font TextFont => new Font("Times", 15);

        public ImageFormat Format => ImageFormat.Jpeg;

        public Size ImageSize => new Size(5000, 5000);


        public Func<List<RectangleWithWord>, string, IVisualization> Visualizator
        {
            get
            {
                return (elementsForVisualization, imageSavePath)
                    => new DefaultVisualization(elementsForVisualization, imageSavePath, Brush, TextFont, Format, ImageSize);
            }
        }
        

        public IWordNormalizer Normalization => new WordNormalizerOrigin();

        public PartsOfSpeech.SpeechPart[] ExcludedParts
        { 
            get => new[]
            {
                PartsOfSpeech.SpeechPart.CONJ, PartsOfSpeech.SpeechPart.INTJ,
                PartsOfSpeech.SpeechPart.PART, PartsOfSpeech.SpeechPart.PR,
            };
        }

        public IAnalyzer Analyzer => new DefaultAnalyzer(ExcludedParts, ExcludedWords);

        public IContentFiller Filler => new CircularCloudLayouterForRectanglesWithText(LayouterCenter, Visualizator, SavePath);
    }
}
