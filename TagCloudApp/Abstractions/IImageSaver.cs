namespace TagCloudApp.Abstractions;

public interface IImageSaver
{
    public string SupportedExtension { get; }
    void SaveImage(Image image);
}