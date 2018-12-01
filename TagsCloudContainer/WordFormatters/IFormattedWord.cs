using System.Drawing;

namespace TagsCloudContainer.WordFormatters
{
    public interface IFormattedWord
    {
        Font Font { get;}

        Color Color { get; }
    }
}