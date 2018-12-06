using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordsCloudVisualizer : IVisualizer<IList<Word>>
    {
        private readonly Color backgroundColor;
        private readonly Brush wordsColor;
        private readonly Size pictureSize;
        
        public WordsCloudVisualizer(Palette palette, Size pictureSize)
        {
            backgroundColor = palette.BackgroundColor;
            wordsColor = palette.SecondaryColor;
            this.pictureSize = pictureSize;
        }
        public Bitmap Draw(IList<Word> words)
        {
            var bmp = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backgroundColor);
                if (words.Count == 0) return bmp;
                foreach (var word in words)
                    g.DrawString(word.Text, word.Font, wordsColor, word.Rectangle.ShiftRectangleToBitMapCenter(bmp));
            }
            return bmp;
        }
    }
}
