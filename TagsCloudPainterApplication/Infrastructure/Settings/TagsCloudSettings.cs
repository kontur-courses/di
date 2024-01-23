using System.ComponentModel;
using TagsCloudPainter.Settings;

namespace TagsCloudPainterApplication.Infrastructure.Settings
{
    public class TagsCloudSettings
    {
        [Browsable(false)] public CloudSettings CloudSettings { get; private set; }
        [Browsable(false)] public TagSettings TagSettings { get; private set; }
        [Browsable(false)] public SpiralPointerSettings SpiralPointerSettings { get; private set; }
        [Browsable(false)] public TextSettings TextSettings { get; private set; }
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

        public TagsCloudSettings(
            CloudSettings cloudSettings,
            TagSettings tagSettings,
            SpiralPointerSettings spiralPointerSettings,
            TextSettings textSettings)
        {
            CloudSettings = cloudSettings;
            TagSettings = tagSettings;
            SpiralPointerSettings = spiralPointerSettings;
            TextSettings = textSettings;
            TagFontSize = 32;
            TagFontName = "Arial";
            CloudCenter = new Point(0, 0);
            PointerStep = 0.1;
            PointerRadiusConst = 0.5;
            PointerAngleConst = 1;
        }
    }
}
