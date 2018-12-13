using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Configuration
{
    public interface IConfiguration
    {
        string PathToWordsFile { get; }
        string DirectoryToSave { get; }
        string OutFileName { get; }
        FontFamily FontFamily { get; }
        Color Color { get; }
        int MinFontSize { get; }
        int MaxFontSize { get; }
        int ImageWidth { get; }
        int ImageHeight { get; }
        ImageFormat ImageFormat { get; }
        int RotationAngle { get; }
        int CenterX { get; }
        int CenterY { get; }
        string BoringWordsFileName { get; }
    }
}