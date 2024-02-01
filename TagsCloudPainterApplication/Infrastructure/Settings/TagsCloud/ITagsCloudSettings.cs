using TagsCloudPainter.Settings;
using TagsCloudPainter.Settings.Cloud;
using TagsCloudPainter.Settings.FormPointer;
using TagsCloudPainter.Settings.Tag;

namespace TagsCloudPainterApplication.Infrastructure.Settings.TagsCloud;

public interface ITagsCloudSettings
{
    public ICloudSettings CloudSettings { get; }
    public ITagSettings TagSettings { get; }
    public ISpiralPointerSettings SpiralPointerSettings { get; }
    public ITextSettings TextSettings { get; }

    public int TagFontSize { get; set; }

    public string TagFontName { get; set; }

    public Point CloudCenter { get; set; }

    public double PointerStep { get; set; }

    public double PointerRadiusConst { get; set; }

    public double PointerAngleConst { get; set; }
}