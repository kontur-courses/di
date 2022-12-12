using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public static class CircularCloudDrawer
    {
        public static void DrawWords(Color[] brushColors,
            Dictionary<string, int> frequencyDictionary,
            IFileSaver fileSaver,
            IUi settings,
            int coefficient,
            ICloudLayouterAlgorithm layouter)
        {
            var canvas = new Bitmap(settings.CanvasWidth, settings.CanvasHeight);
            var graphics = Graphics.FromImage(canvas);
            var color = Color.FromName(settings.BackGroundColor);
            if (!color.IsKnownColor)
                throw new ArgumentException("Unknown background color");
            graphics.Clear(color);
            var counter = 0;
            foreach (var pair in frequencyDictionary)
            {
                var word = pair.Key;
                var wordCount = pair.Value;
                var rectangleHeight = wordCount * coefficient * word.Length + coefficient;
                var rectangleWidth = wordCount * 2 * coefficient;
                var rectangle = layouter.PutNextRectangle(new Size(rectangleHeight, rectangleWidth));
                var font = new Font(settings.FontName, (coefficient + 2) * wordCount - 2);
                if (font.Name != font.OriginalFontName)
                    throw new ArgumentException("Unknown font name");
                graphics.DrawString(word, new Font(settings.FontName, (coefficient + 2) * wordCount - 2),
                    new SolidBrush(brushColors[counter]), rectangle);
                counter++;
            }

            fileSaver.SaveCanvas(settings.PathToSave, canvas);
        }
    }
}