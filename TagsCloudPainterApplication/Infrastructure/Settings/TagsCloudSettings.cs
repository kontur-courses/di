using System.ComponentModel;
using System.Xml.Linq;
using TagsCloudPainter.Settings;

namespace TagsCloudPainterApplication.Infrastructure.Settings;

public class TagsCloudSettings
{
    public TagsCloudSettings(
        CloudSettings cloudSettings,
        TagSettings tagSettings,
        SpiralPointerSettings spiralPointerSettings,
        TextSettings textSettings)
    {
        CloudSettings = cloudSettings ?? throw new ArgumentNullException(nameof(cloudSettings));
        TagSettings = tagSettings ?? throw new ArgumentNullException(nameof(tagSettings));
        SpiralPointerSettings = spiralPointerSettings ?? throw new ArgumentNullException(nameof(spiralPointerSettings));
        TextSettings = textSettings ?? throw new ArgumentNullException(nameof(textSettings));
        TagFontSize = 32;
        TagFontName = "Arial";
        CloudCenter = new Point(0, 0);
        PointerStep = 0.1;
        PointerRadiusConst = 0.5;
        PointerAngleConst = 1;
    }

    [Browsable(false)] public CloudSettings CloudSettings { get; }
    [Browsable(false)] public TagSettings TagSettings { get; }
    [Browsable(false)] public SpiralPointerSettings SpiralPointerSettings { get; }
    [Browsable(false)] public TextSettings TextSettings { get; }

    public int TagFontSize
    {
        get => TagSettings.TagFontSize;
        set => TagSettings.TagFontSize = value;
    }

    public string TagFontName
    {
        get => TagSettings.TagFontName;
        set => TagSettings.TagFontName = value;
    }

    public Point CloudCenter
    {
        get => CloudSettings.CloudCenter;
        set => CloudSettings.CloudCenter = value;
    }

    public double PointerStep
    {
        get => SpiralPointerSettings.Step;
        set => SpiralPointerSettings.Step = value;
    }

    public double PointerRadiusConst
    {
        get => SpiralPointerSettings.RadiusConst;
        set => SpiralPointerSettings.RadiusConst = value;
    }

    public double PointerAngleConst
    {
        get => SpiralPointerSettings.AngleConst;
        set => SpiralPointerSettings.AngleConst = value;
    }
}