using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter.Filler;
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

        //var path = Path.GetDirectoryName(typeof(Program).Assembly.Location);
        //Для выделения пути к каталогу, воспользуйтесь `System.IO.Path`:
        //var path = Path.GetDirectoryName(location);

        //Console.WriteLine(path);
        //public string MystemPath => "C:/GitHub/di/TagsCloudVisualizationDI/mystem.exe";

        public string MystemPath => Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\mystem.exe";

        //public string SaveAnalizationPath => "C:/GitHub/di/TagsCloudVisualizationDI/result.TXT";
        public string SaveAnalizationPath => Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\result.TXT";



        public string SavePath => throw new NotImplementedException();
        public string FilePath => throw new NotImplementedException();
        public List<string> ExcludedWords => new List<string>();
        public ImageFormat Format => ImageFormat.Png;



        public string Args => "-lndw -ig";

        public Encoding TextEncoding => Encoding.UTF8;
        

        public ITextFileReader FileReader => new DefaultTextFileReader(FilePath, SaveAnalizationPath, MystemPath, Args, TextEncoding);

        public SolidBrush Brush => new SolidBrush(Color.White);

        public Font TextFont => new Font("Times", 15);

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
