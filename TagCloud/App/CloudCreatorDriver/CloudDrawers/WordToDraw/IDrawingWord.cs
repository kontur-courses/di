using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Word;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.WordToDraw
{
    public interface IDrawingWord : IWord
    {
        public Font Font { get; }
        public Color Color { get; }
        public Rectangle Rectangle { get; }
    }
}