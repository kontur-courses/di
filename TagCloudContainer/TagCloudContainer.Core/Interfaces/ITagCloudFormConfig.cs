namespace TagCloudContainer.Core.Interfaces;

public interface ITagCloudFormConfig
{
    public string FontFamily { get; set; }
    public Color Color { get; set; }
    public Color BackgroundColor { get; set; }
    public Size ImageSize { get; set; } 
}