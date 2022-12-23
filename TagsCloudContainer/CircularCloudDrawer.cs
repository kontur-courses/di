using System;
using System.Drawing;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public static class CircularCloudDrawer
    {
        public static void DrawWords(IUi settings, Configuration configuration)
        {
            var canvas = new Bitmap(settings.CanvasWidth, settings.CanvasHeight);
            var graphics = Graphics.FromImage(canvas);
            var color = configuration.BackgroundColor;
            graphics.Clear(color);
            var frequencyDictionary =
                InputFileHandler.FormFrequencyDictionary(configuration.FileReader.FileToWordsArray(settings.PathToOpen),
                    settings);
            var wordColorDictionary =
                configuration.Painter.GetWordColorDictionary(frequencyDictionary, configuration.BrushColor);
            foreach (var pair in frequencyDictionary)
            {
                var word = pair.Key;
                var wordCount = pair.Value;
                var rectangleHeight = wordCount * configuration.Coefficient * word.Length + configuration.Coefficient;
                var rectangleWidth = wordCount * 2 * configuration.Coefficient;
                var rectangle = configuration.Algorithm.PutNextRectangle(new Size(rectangleHeight, rectangleWidth));
                try
                {
                    graphics.DrawString(word, configuration.GetFontFromString(wordCount),
                        new SolidBrush(wordColorDictionary[word]), rectangle);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            configuration.FileSaver.SaveCanvas(settings.PathToSave, canvas);
            graphics.Dispose();
        }
    }
}