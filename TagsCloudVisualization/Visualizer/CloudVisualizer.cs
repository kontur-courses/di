using System.Drawing;
using TagsCloudVisualization.ImageRendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.FontSettings;
using TagsCloudVisualization.Frequency;

namespace TagsCloudVisualization.Visualizer 
{
    public class CloudVisualizer : IImageVisualizer
    {
        public Point Center { get; }
        private readonly Bitmap image;
        private readonly Dictionary<string, int> wordsFrequency;
        private readonly Graphics gr;
        private readonly ICloudLayouter layouter;
        private readonly FontFamily fontFamily;
        private readonly string fontColor;

        public CloudVisualizer(
            ICloudLayouter layouter,
            IFrequencyCounter frequencyCounter,
            IImageRenderingSettings settings,
            IFontSettings fontSettings)
        {
            if (layouter == null) throw new ArgumentNullException(nameof(layouter));
            Center = layouter.Spiral.Center;
            wordsFrequency = frequencyCounter.GetFrequency();
            this.layouter = layouter;
            image = new Bitmap(settings.Width, settings.Height);
            gr = Graphics.FromImage(image);
            fontFamily = fontSettings.FontFamily;
            fontColor = fontSettings.FontColor;
        }

        public Image CreateImage()
        {
            gr.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, image.Width, image.Height));
            DrawWords();
            return image;
        }

        private void DrawWords()
        {
            double minFrequency = wordsFrequency.Min(x => x.Value);
            double maxFrequency = wordsFrequency.Max(x => x.Value);
            var brush = new SolidBrush(GetColor());

            foreach (var wordFreqPair in wordsFrequency.OrderByDescending(x => x.Value))
            {
                var font = GetFont(12, 50, minFrequency, maxFrequency, wordFreqPair.Value);
                var rectSize = GetWordSize(wordFreqPair.Key, font);
                var rect = layouter.PutNextRectangle(rectSize);
                var stringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                };

                gr.DrawString(wordFreqPair.Key, font, brush, rect, stringFormat);

                if (fontColor == "random")
                    brush.Color = GetRandomColor();
            }
        }

        private Color GetColor()
        {
            if (fontColor == "random")
                return GetRandomColor();
            
            return ParseColor();
        }

        private Color ParseColor()
        {
            var separators = new[] { ',', '.', ' ' };
            var argb = fontColor.Split(separators).Select(x => int.Parse(x)).ToArray();
            return Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);
        }

        private Font GetFont(int minSize, int maxSize, double minFrequency, double maxFrequency, double wordFrequency)
        {
            var fontSize =
                (int)(minSize + (maxSize - minSize) * (wordFrequency - minFrequency) / (maxFrequency - minFrequency));
            return new Font(fontFamily, fontSize);
        }

        private static Color GetRandomColor()
        {
            var random = new Random();
            return Color.FromArgb(
                (byte)random.Next(0, 255),
                (byte)random.Next(0, 255),
                (byte)random.Next(0, 255));
        }

        private Size GetWordSize(string word, Font font)
        {
            var wordSize = gr.MeasureString(word, font);
            var width = (int)Math.Ceiling(wordSize.Width);
            var height = (int)Math.Ceiling(wordSize.Height);

            return new Size(width, height);
        }
    }
}





