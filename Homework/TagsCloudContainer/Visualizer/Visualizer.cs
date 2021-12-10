using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.Visualizer
{
    public class Visualizer : IVisualizer, IDisposable
    {
        private readonly ICloudLayouter layouter;
        
        private readonly Size imageSize;
        private readonly IColorGenerator wordsColorsGenerator;
        private readonly Font font;
        private readonly Bitmap bmp;
        private readonly Graphics graphics;
        private readonly Brush brush;

        public Visualizer(IVisualizerSettings settings, ICloudLayouter layouter)
        {
            imageSize = settings.ImageSize;
            bmp = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bmp);
            graphics.Clear(settings.BackgroundColor);
            brush = new SolidBrush(Color.White);
            wordsColorsGenerator = settings.WordsColorGenerator;
            font = settings.Font;
            this.layouter = layouter;
        }

        public Bitmap Visualize(Dictionary<string, int> freqDict)
        {
            var wordsCount = freqDict.Count;
            var wordsColors = wordsColorsGenerator.GetColors(wordsCount);
            var orderedFreqPairs = freqDict
                .OrderByDescending(kv => kv.Value)
                .ToArray();
            var mostFreq = orderedFreqPairs.First().Value;
            foreach (var (word, freq) in orderedFreqPairs)
                VisualizeWord(word, freq, mostFreq, wordsColors.Pop());
            return bmp;
        }

        public void Dispose()
        {
            font.Dispose();
            graphics.Dispose();
            brush.Dispose();
        }

        private void VisualizeWord(string word, int freq, int mostFreq, Color color)
        {
            var freqDelta = mostFreq - freq;
            using var newFont = new Font(font.FontFamily, Math.Min(font.Size - freqDelta, 5));
            var rectSize = new Size((int)newFont.Size * word.Length, newFont.Height);
            var layoutRectangle = layouter.PutNextRectangle(rectSize);
            using var brush = new SolidBrush(color);
            graphics.DrawString(word, newFont, brush, layoutRectangle);
        }
        
    }
}