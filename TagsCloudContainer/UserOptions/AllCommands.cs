using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class AllCommands : RequiredUserCommands
    {
        [Name("f", "font"), Description("Text font, underscores separated font name and size")]
        public string Font { get; set; }

        [Name("s", "size"), Description("Image size with int parameters, underscores separated width and height")]
        public string ImageSize { get; set; }

        [Name("tc", "textColor"), Description("Text color")]
        public string TextColor { get; set; }

        [Name("bgc", "backColor"), Description("Background color")]
        public string BackgroundColor { get; set; }

        [Name("bw", "boring"), Description("Boring words, underscores separated between words")]
        public string BoringWords { get; set; }

        [Name("if", "imageFormat"),
         Description("Image format. Supported formats: \"bmp\", \"jpeg\", \"png\", \"gif\", \"tiff\"")]
        public string CloudImageFormat { get; set; }
    }
}