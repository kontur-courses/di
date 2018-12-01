using System.Drawing;

namespace TagsCloudContainer.WordLayouts
{
    public interface IWordLayout
    {
        IPositionedWord PositionNextWord(IPositionedWord word, SizeF size);
    }
}