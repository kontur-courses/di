using System.Drawing;

namespace TagsCloud.Core.BitmapPainters.Pallets;

public interface ITagCLoudPallet
{
    public Color BackgroundColor { get; }
    public Color GetNextColor();
}