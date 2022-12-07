using System.Drawing;
using TagsCloudVisualisation.App.WordsPreprocessor.Word;

namespace TagsCloudVisualisation.App.CloudDrawers.WordToDraw
{
    public class DrawingWord : IDrawingWord
    {
        public string Value { get; }
        public int Count { get; set; }
        public double Tf { get; set; }
        public Font Font { get; }
        public Color Color { get; }
        public Rectangle Rectangle { get; }

        public DrawingWord(string value, int count, double tf, Font font, Color color, Rectangle rectangle)
        {
            Value = value;
            Count = count;
            Tf = tf;
            Font = font;
            Color = color;
            Rectangle = rectangle;
        }
        
        public DrawingWord(IWord word, Font font, Color color, Rectangle rectangle)
        {
            Value = word.Value;
            Count = word.Count;
            Tf = word.Tf;
            Font = font;
            Color = color;
            Rectangle = rectangle;
        }

        public bool Equals(IWord other)
        {
            return ((IWord)this).Equals(other);
        }
    }
}