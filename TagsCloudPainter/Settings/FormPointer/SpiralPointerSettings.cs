namespace TagsCloudPainter.Settings.FormPointer;

public class SpiralPointerSettings : ISpiralPointerSettings
{
    public double Step { get; set; } = 1;
    public double RadiusConst { get; set; } = 1;
    public double AngleConst { get; set; } = 1;
}