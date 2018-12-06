
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.TagsCloud.CircularCloud
{
    public class TagsCloudVisualizer
    {
        public int MinFont { get; set; }
        private readonly TagsCloudSettings cloudSettings;
        public TagsCloudVisualizer(TagsCloudSettings cloudSettings)
        {
            this.cloudSettings = cloudSettings;
            MinFont = (int)(cloudSettings.ImageSize.Width / 33.0);
        }
        public Bitmap DrawCircularCloud()
        {
            var bmp = new Bitmap(cloudSettings.ImageSize.Width, cloudSettings.ImageSize.Height);
            var gr = Graphics.FromImage(bmp);
            var processedWords = ProcessWords(cloudSettings.WordFrequencyDictionary);
            gr.Clear(Color.AliceBlue);
            foreach (var processedWord in processedWords)
            {
                var font = new Font("Times New Roman", MinFont * processedWord.NumberRepetitions,
                    FontStyle.Regular, GraphicsUnit.Pixel);
                gr.DrawString(processedWord.word, font, Brushes.BlueViolet, processedWord.Region);
            }
            return bmp;
        }

        public IEnumerable<(Rectangle Region, string word, int NumberRepetitions)>
            ProcessWords(Dictionary<string, int> wordFrequencyDictionary)
        {
            var circularCloudLayouter = new CircularCloudLayouter(cloudSettings.Center, cloudSettings.ImageSize);
            return wordFrequencyDictionary.OrderByDescending(pair => pair.Value)
                .Select(pair => (circularCloudLayouter.PutNextRectangle
                    (new Size((int)((14.0 / 20) * pair.Key.Length * MinFont * pair.Value),
                    (int)(1.2 * MinFont * pair.Value))), pair.Key, pair.Value));
        }
    }
}