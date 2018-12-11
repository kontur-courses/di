using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization.TagsCloud.CircularCloud
{
    public class TagsCloudVisualizer
    {
        private readonly TagsCloudSettings cloudSettings;
        private const double HeightStretchFactor = 1.5;
        public TagsCloudVisualizer(TagsCloudSettings cloudSettings)
        {
            this.cloudSettings = cloudSettings;
        }
        public Bitmap DrawCircularCloud()
        {
            var imageSettings = cloudSettings.ImageSettings;
            var bmp = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var graphics = Graphics.FromImage(bmp);
            var processedWords = ProcessWords(cloudSettings.FrequenciesByWords);
            var wordsColor = cloudSettings.Palette.WordsColor;
            var font = imageSettings.Font;
            const TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping;

            graphics.Clear(cloudSettings.Palette.BackgroundColor);
            foreach (var processedWord in processedWords)
            {
                var enlargedFont = new Font(font.Name, font.Size * processedWord.RepetitionsCount,
                    font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
                TextRenderer.DrawText(graphics, processedWord.word, enlargedFont, processedWord.Region, wordsColor, flags);
            }
            return bmp;
        }

        private IEnumerable<(Rectangle Region, string word, int RepetitionsCount)>
            ProcessWords(Dictionary<string, int> frequenciesByWords)
        {
            var imageSettings = cloudSettings.ImageSettings;
            var circularCloudLayouter = new CircularCloudLayouter(imageSettings.Center, imageSettings.ImageSize);

            return frequenciesByWords.OrderByDescending(pair => pair.Key.Length)
                .OrderByDescending(pair => pair.Value)
                .Select(pair => (Word: pair.Key, Frequency: pair.Value))
                .Select(tuple => (GetRectangle(tuple, circularCloudLayouter), tuple.Word, tuple.Frequency));
        }

        private Rectangle GetRectangle((string Word, int Frequency) tuple, CircularCloudLayouter circularCloudLayouter)
        {
            var fontSize = cloudSettings.ImageSettings.Font.Size;

            return circularCloudLayouter.PutNextRectangle
                                (new Size((int)(tuple.Word.Length * fontSize * tuple.Frequency),
                                (int)(fontSize * HeightStretchFactor * tuple.Frequency)));
        }
    }
}