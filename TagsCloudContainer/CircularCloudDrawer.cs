using System;
using System.Drawing;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagsCloudContainer
{
    public static class CircularCloudDrawer
    {
        public static void DrawWords(
            WordsColoringFactory wordsColoringFactory,
            FileSaverFactory fileSaverFactory,
            FileReaderFactory fileReaderFactory,
            IUi settings,
            LayouterFactory layouterFactory)
        {
            var painter = wordsColoringFactory.Create();
            var fileSaver = fileSaverFactory.Create();
            var fileReader = fileReaderFactory.Create();
            var layouter = layouterFactory.Create(new Spiral(settings));
            var canvas = new Bitmap(settings.CanvasWidth, settings.CanvasHeight);
            var graphics = Graphics.FromImage(canvas);
            var color = GetColorFromString(settings.BackGroundColor);
            graphics.Clear(color);
            var counter = 0;
            var coefficient = ScaleCoefficientCalculator.CalculateScaleCoefficient(settings.CanvasWidth,
                settings.CanvasHeight, settings.CanvasBorder);
            var frequencyDictionary =
                InputFileHandler.FormFrequencyDictionary(fileReader.FileToWordsArray(settings.PathToOpen), settings);
            var brushColors = painter.GetColorsSequence(frequencyDictionary, GetColorFromString(settings.BrushColor));
            foreach (var pair in frequencyDictionary)
            {
                var word = pair.Key;
                var wordCount = pair.Value;
                var rectangleHeight = wordCount * coefficient * word.Length + coefficient;
                var rectangleWidth = wordCount * 2 * coefficient;
                var location = layouter.PlaceNextWord(word, wordCount, coefficient);
                var rectangle = new Rectangle(location, new Size(rectangleHeight, rectangleWidth));
                try
                {
                    var font = GetFontFromString(coefficient, settings.FontName, wordCount);
                    graphics.DrawString(word, font, new SolidBrush(brushColors[counter]), rectangle);
                    counter++;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            fileSaver.SaveCanvas(settings.PathToSave, canvas);
            graphics.Dispose();
        }

        private static Color GetColorFromString(string color)
        {
            var result = Color.FromName(color);
            if (!result.IsKnownColor)
                throw new ArgumentException("Unknown color");
            return result;
        }

        private static Font GetFontFromString(int coefficient, string fontName, int wordCount)
        {
            var font = new Font(fontName, (coefficient + 2) * wordCount - 2);
            if (font.Name != font.OriginalFontName)
                throw new ArgumentException("Unknown font name");
            return font;
        }
    }
}