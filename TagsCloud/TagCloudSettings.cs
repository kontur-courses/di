using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud
{
    public class TagCloudSettings
    {
        public readonly string PathToInput;
        public readonly string PathToOutput;
        public readonly string PathToBoringWords;
        public readonly int WidthOutputImage;
        public readonly int HeightOutputImage;
        public readonly Color BackgroundColor;
        public readonly string FontName;
        public readonly ImageFormat imageFormat;

        public TagCloudSettings(string PathToInput,
            string PathToOutput,
            string PathToBoringWords,
            int WidthOutputImage,
            int HeightOutputImage,
            Color BackgroundColor,
            string FontName)
        {
            this.PathToInput = PathToInput;
            this.PathToOutput = PathToOutput;
            this.PathToBoringWords = PathToBoringWords;
            this.WidthOutputImage = WidthOutputImage;
            this.HeightOutputImage = HeightOutputImage;
            this.BackgroundColor = BackgroundColor;
            this.FontName = FontName;
            this.imageFormat = ImageFormat.Png;
        }
    }
}
