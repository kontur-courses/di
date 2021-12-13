using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using TagsCloudVisualizationDI.AnalyzedTextReader;
using TagsCloudVisualizationDI.Layouter.Filler;
using TagsCloudVisualizationDI.Saving;
using TagsCloudVisualizationDI.TextAnalization;
using TagsCloudVisualizationDI.TextAnalization.Analyzer;
using TagsCloudVisualizationDI.TextAnalization.Normalizer;
using TagsCloudVisualizationDI.TextAnalization.Visualization;

namespace TagsCloudVisualizationDI.Settings
{
    public interface ISettingsConfiguration
    {
        public Size ElementSize => new Size(100, 100);
        public Point LayouterCenter => new Point(2500, 2500);
        public string MystemPath => Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\mystem.exe";
        public string SaveAnalizationPath => Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\result.TXT";



        public string SavePath
        { get; }

        public string FilePath
        { get; }
        public List<string> ExcludedWords => new List<string>();
        public ImageFormat Format => ImageFormat.Png;



        public string Args => "-lndw -ig";

        public Encoding TextEncoding => Encoding.UTF8;


        public ITextFileReader FileReader =>
            new DefaultTextFileReader(SaveAnalizationPath, TextEncoding);

        public SolidBrush Brush => new SolidBrush(Color.White);

        public Font TextFont => new Font("Times", 15);

        public Size ImageSize => new Size(5000, 5000);

        public ISaver Saver => new DefaultSaver(SavePath, Format);


        public IVisualization Visualizator
        {
            get => new DefaultVisualization(Brush, TextFont, ImageSize, 25);
        }

        public INormalizer Normalization => new DefaultNormalizer();

        public PartsOfSpeech.SpeechPart[] ExcludedParts
        {
            get => new[]
            {
                PartsOfSpeech.SpeechPart.CONJ, PartsOfSpeech.SpeechPart.INTJ,
                PartsOfSpeech.SpeechPart.PART, PartsOfSpeech.SpeechPart.PR,
            };
        }

        public IAnalyzer Analyzer => new DefaultAnalyzer(ExcludedParts, ExcludedWords, FilePath, SaveAnalizationPath, MystemPath, Args);

        public IContentFiller Filler => new CircularCloudLayouterForRectanglesWithText(LayouterCenter);
    }
}
