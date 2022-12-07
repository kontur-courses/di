using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Options;

public class VisualizationOptions
{
    public ImageFormat SavingImageFormat { get; }
    public string PathToTextFile { get; }
    public Size ImageSize { get; }
    public int BoringWordsThreshold { get; }
    public int MinFontSize { get; }

    public VisualizationOptions(string pathToTextFile, ImageFormat savingImageFormat, Size imageSize, 
        int boringWordsThreshold, int minFontSize)
    {
        SavingImageFormat = savingImageFormat;
        PathToTextFile = pathToTextFile;
        ImageSize = imageSize;
        BoringWordsThreshold = boringWordsThreshold;
        MinFontSize = minFontSize;
    }
}