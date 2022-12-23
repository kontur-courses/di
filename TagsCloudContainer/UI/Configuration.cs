using System;
using System.Drawing;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagsCloudContainer.UI
{
    public class Configuration
    {
        public int Coefficient { get; }
        private string FontName { get; }
        public Color BackgroundColor { get; }
        public Color BrushColor { get; }
        public IFileSaver FileSaver { get; }
        public IWordsPainter Painter { get; }
        public IFileReader FileReader { get; }
        public ICloudLayouterAlgorithm Algorithm { get; }

        public Configuration(IUi settings,
            WordsColoringFactory wordsColoringFactory,
            FileSaverFactory fileSaverFactory,
            FileReaderFactory fileReaderFactory,
            LayouterFactory layouterFactory)
        {
            CheckArguments(settings);
            Coefficient = ScaleCoefficientCalculator.CalculateScaleCoefficient(settings.CanvasWidth,
                settings.CanvasHeight, settings.CanvasBorder);
            FontName = CheckFontName(settings.FontName);
            BackgroundColor = GetColorFromString(settings.BackGroundColor);
            BrushColor = GetColorFromString(settings.BrushColor);
            FileSaver = fileSaverFactory.Create();
            Painter = wordsColoringFactory.Create();
            FileReader = fileReaderFactory.Create();
            Algorithm = layouterFactory.Create();
        }

        private string CheckFontName(string settingsFontName)
        {
            var testFont = new Font(settingsFontName, 10);
            if (testFont.Name != testFont.OriginalFontName)
                throw new ArgumentException("Unknown font name");
            return settingsFontName;
        }

        public Font GetFontFromString(int wordCount)
        {
            var font = new Font(FontName, (Coefficient + 2) * wordCount - 2);
            if (font.Name != font.OriginalFontName)
                throw new ArgumentException("Unknown font name");
            return font;
        }

        private static Color GetColorFromString(string color)
        {
            var result = Color.FromName(color);
            if (!result.IsKnownColor)
                throw new ArgumentException("Unknown color");
            return result;
        }

        private static void CheckArguments(IUi parsedArguments)
        {
            if (parsedArguments.CanvasBorder < 0)
                throw new ArgumentException("Borders can't be less than zero");
            if (parsedArguments.CanvasHeight < parsedArguments.CanvasBorder * 2)
                throw new ArgumentException("Too small canvas height");
            if (parsedArguments.CanvasWidth < parsedArguments.CanvasBorder * 2)
                throw new ArgumentException("Too small canvas width");
            if (parsedArguments.AngleOffset > 1 || parsedArguments.AngleOffset < 0)
                throw new ArgumentException("Invalid angle offset");
            if (parsedArguments.RadiusOffset > 1 || parsedArguments.RadiusOffset < 0)
                throw new ArgumentException("Invalid radius offset");
        }
    }
}