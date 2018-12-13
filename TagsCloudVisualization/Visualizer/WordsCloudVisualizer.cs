using System.Drawing;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization.Visualizer
{
    public class WordsCloudVisualizer : IVisualizer<IWordsCloudBuilder>
    {
        private readonly Color backgroundColor;
        private readonly Brush wordsColor;
        private readonly IWordsCloudBuilder wordsCloudBuilder;
        private readonly Size pictureSize;
        
        public WordsCloudVisualizer(IWordsCloudBuilder wordsCloudBuilder, Palette palette, Size pictureSize)
        {
            backgroundColor = palette.BackgroundColor;
            wordsColor = palette.SecondaryColor;
            this.pictureSize = pictureSize;
            this.wordsCloudBuilder = wordsCloudBuilder;
        }
        public Bitmap Draw()
        {
            var words = wordsCloudBuilder.Build();
            var bmp = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backgroundColor);
                foreach (var word in words)
                    g.DrawString(word.Text, word.Font, wordsColor, word.Rectangle.ShiftRectangleToBitMapCenter(bmp));
            }
            return bmp;
        }
    }
}
