using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class RandomColoredDrawerSettings : DrawerSettings
{
    public Rectangle ImageRectangle { get; set; } = new(0, 0, 1000, 1000);

    public int NumbersSize { get; set; } = 8;

    public float MinimumTextFontSize { get; set; } = 16;

    public float MaximumTextFontSize { get; set; } = 24;

    public int RectangleBorderSize { get; set; } = 2;

    public bool FillRectangles { get; set; } = true;
}