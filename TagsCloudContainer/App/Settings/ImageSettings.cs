using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.Settings
{
    public class ImageSettings
    {
        public readonly string FontName;
        public readonly ImageFormat Format;
        public readonly Size ImageSize;
        public readonly string InputFileName;
        public readonly CloudLayouterAlgorithm LayouterAlgorithm;
        public readonly Color TextColor;

        public ImageSettings(Size imageSize, string fontName,
            Color textColor, ImageFormat imageFormat, string inputFileName,
            CloudLayouterAlgorithm layouterAlgorithm)
        {
            ImageSize = imageSize;
            FontName = fontName;
            TextColor = textColor;
            Format = imageFormat;
            InputFileName = inputFileName;
            LayouterAlgorithm = layouterAlgorithm;
        }

        public static ImageSettings GetDefaultSettings()
        {
            return new ImageSettings(new Size(500, 500), "Arial",
                Color.White, ImageFormat.Bmp, Path.Combine(
                    Directory.GetCurrentDirectory(), "..", "..", "..", "text.txt"),
                CloudLayouterAlgorithm.CircularCloudLayouter);
        }

        public ImageSettings Using(CloudLayouterAlgorithm algorithm)
        {
            return new ImageSettings(ImageSize, FontName, TextColor, Format, InputFileName, algorithm);
        }
    }
}