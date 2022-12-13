using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ClassicDrawerSettings : DrawerSettings
{
    public Rectangle ImageRectangle { get; set; } = new(0, 0, 1000, 1000);

    public int NumbersSize { get; set; } = 8;

    public string NumbersFamily { get; set; } = "Times New Roman";

    public Brush NumbersBrush { get; set; } = new SolidBrush(Color.GhostWhite);

    public Brush TextBrush { get; set; } = new SolidBrush(Color.White);

    public string TextFontFamily { get; set; } = "Comic Sans MS";

    public FontStyle TextFontStyle { get; set; } = FontStyle.Italic;

    public float MinimumTextFontSize { get; set; } = 16;

    public float MaximumTextFontSize { get; set; } = 24;

    public bool WriteNumbers { get; set; } = true;

    public Brush RectangleBorderBrush { get; set; } = new SolidBrush(Color.Red);

    public int RectangleBorderSize { get; set; } = 2;

    public Brush RectangleFillBrush { get; set; } = new SolidBrush(Color.Pink);

    public bool FillRectangles { get; set; } = true;

    public Brush BackgroundBrush { get; set; } = Brushes.LightGoldenrodYellow;
}