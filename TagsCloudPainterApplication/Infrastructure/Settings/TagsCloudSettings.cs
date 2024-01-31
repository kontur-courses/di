using System.ComponentModel;
using TagsCloudPainter.Settings;
using TagsCloudPainterApplication.Properties;

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
        TagFontSize = AppSettings.Default.tagFontSize;
        TagFontName = AppSettings.Default.tagFontName;
        PointerStep = AppSettings.Default.pointerStep;
        PointerRadiusConst = AppSettings.Default.pointerRadiusConst;
        PointerAngleConst = AppSettings.Default.pointerAngleConst;
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
        set => TagSettings.TagFontName = value ?? TagSettings.TagFontName;
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