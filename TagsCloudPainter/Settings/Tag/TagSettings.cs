using System.Drawing;
using System.Drawing.Text;

namespace TagsCloudPainter.Settings.Tag;

public class TagSettings : ITagSettings
{
    public int TagFontSize { get; set; }
    public string TagFontName { get; set; } = new InstalledFontCollection().Families.FirstOrDefault().Name;
    public Color TagColor { get; set; }
}