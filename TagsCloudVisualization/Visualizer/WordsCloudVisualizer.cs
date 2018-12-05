using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordsCloudVisualizer : IVisualizer<Word>
    {
        private readonly Color backgroundColor;
        private readonly Brush wordsColor;
        private readonly Size pictureSize;
        
        public WordsCloudVisualizer(Palette palette, Size size)
        {
            this.backgroundColor = palette.BackgroundColor;
            this.wordsColor = palette.SecondaryColor;
            this.pictureSize = size;
        }
        public Bitmap Draw(IList<Word> words)
        {
            var bmp = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backgroundColor);
                if (!words.Any()) return bmp;
                foreach (var word in words)
                    g.DrawString(word.Text, word.Font, wordsColor, word.Rectangle.ShiftRectangleToBitMapCenter(bmp));
            }
            return bmp;
        }
    }
}
