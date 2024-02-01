using System.Drawing;

namespace TagsCloudPainter.Settings.Tag;

public interface ITagSettings
{
    public int TagFontSize { get; set; }
    public string TagFontName { get; set; }
    public Color TagColor { get; set; }
}