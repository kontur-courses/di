using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public interface ITagDrawingSettingsProvider
    {
        FontStyle FontStyle { get; set; }
        FontFamily FontFamily { get; set; }
        float MaxFontSize { get; set; }
        float MinFontSize { get; set; }
    }
}