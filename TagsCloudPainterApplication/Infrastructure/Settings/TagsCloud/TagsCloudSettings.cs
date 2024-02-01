using System.ComponentModel;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Settings.Cloud;
using TagsCloudPainter.Settings.FormPointer;
using TagsCloudPainter.Settings.Tag;
using TagsCloudPainterApplication.Properties;

namespace TagsCloudPainterApplication.Infrastructure.Settings.TagsCloud;

public class TagsCloudSettings : ITagsCloudSettings
{
    public TagsCloudSettings(
        ICloudSettings cloudSettings,
        ITagSettings tagSettings,
        ISpiralPointerSettings spiralPointerSettings,
        ITextSettings textSettings,
        IAppSettings appSettings)
    {
        CloudSettings = cloudSettings ?? throw new ArgumentNullException(nameof(cloudSettings));
        TagSettings = tagSettings ?? throw new ArgumentNullException(nameof(tagSettings));
        SpiralPointerSettings = spiralPointerSettings ?? throw new ArgumentNullException(nameof(spiralPointerSettings));
        TextSettings = textSettings ?? throw new ArgumentNullException(nameof(textSettings));
        TagFontSize = appSettings.TagFontSize;
        TagFontName = appSettings.TagFontName;
        PointerStep = appSettings.PointerStep;
        PointerRadiusConst = appSettings.PointerRadiusConst;
        PointerAngleConst = appSettings.PointerAngleConst;
    }

    [Browsable(false)] public ICloudSettings CloudSettings { get; }
    [Browsable(false)] public ITagSettings TagSettings { get; }
    [Browsable(false)] public ISpiralPointerSettings SpiralPointerSettings { get; }
    [Browsable(false)] public ITextSettings TextSettings { get; }

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