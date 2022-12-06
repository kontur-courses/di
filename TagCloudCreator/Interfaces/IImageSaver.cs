using System.Drawing;

namespace TagCloudCreator.Interfaces;

public interface IImageSaver
{
    public string SupportedExtension { get; }
    void SaveImage(Image image);
}