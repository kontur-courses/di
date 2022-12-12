using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers;

public interface IDrawingWord : IWord
{
    Font Font { get; }
    Color Color { get; }
    Rectangle Rectangle { get; }
}