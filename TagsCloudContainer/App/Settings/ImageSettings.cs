using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.Settings
{
    public class ImageSettings
    {
        public string FontName { get; set; }
        public ImageFormat Format { get; set; }
        public Size ImageSize { get; set; }
        public string InputFileName { get; set; }
        public CloudLayouterAlgorithm LayouterAlgorithm { get; set; }
        public Color TextColor { get; set; }

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