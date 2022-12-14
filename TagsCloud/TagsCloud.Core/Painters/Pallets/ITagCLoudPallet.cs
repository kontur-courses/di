using System.Drawing;

namespace TagsCloud.Core.Painters.Pallets;

public interface ITagCLoudPallet
{
    public Color BackgroundColor { get; }

    public Color GetNextColor();
}