using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Rendering
{
    public class RenderConfig
    {
        public string OutputFile { get; }
        public ImageFormat ImageFormat { get; }
        public Size? DesiredImageSize { get; init; }
        public float? Scale { get; init; }
        public Brush Background { get; init; } = new SolidBrush(Color.Transparent);

        public RenderConfig(string outputFile, ImageFormat imageFormat)
        {
            OutputFile = outputFile ?? throw new ArgumentNullException(nameof(outputFile));
            ImageFormat = imageFormat ?? throw new ArgumentNullException(nameof(imageFormat));
        }
    }
}