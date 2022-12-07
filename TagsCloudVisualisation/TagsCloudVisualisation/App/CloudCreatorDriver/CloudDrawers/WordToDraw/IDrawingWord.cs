using System.Drawing;
using TagsCloudVisualisation.App.WordsPreprocessor.Word;

namespace TagsCloudVisualisation.App.CloudDrawers.WordToDraw
{
    public interface IDrawingWord : IWord
    {
        public Font Font { get; }
        public Color Color { get; }
        public Rectangle Rectangle { get; }
    }
}