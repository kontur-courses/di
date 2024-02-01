namespace TagsCloudPainterApplication.Properties;

public interface IAppSettings
{
    public string BoringTextFilePath { get; set; }
    public int ImageWidth { get; set; }
    public int ImageHeight { get; set; }
    public int TagFontSize { get; set; }
    public string TagFontName { get; set; }
    public double PointerStep { get; set; }
    public double PointerRadiusConst { get; set; }
    public double PointerAngleConst { get; set; }
}