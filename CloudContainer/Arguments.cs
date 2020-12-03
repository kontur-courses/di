using MatthiWare.CommandLine.Core.Attributes;

namespace CloudContainer
{
    public class Arguments
    {
        public Arguments(string font, string size, string center, string color)
        {
            Font = font;
            ImageSize = size;
            Center = center;
            Color = color;
        }

        public Arguments()
        {
        }

        [Name("f", "font")]
        [Description("Text font, underscores separated font name and size")]
        public string Font { get; set; }

        [Name("s", "size")]
        [Description("Image size with int parameters, underscores separated width and height")]
        public string ImageSize { get; set; }

        [Name("ce", "center")]
        [Description("Center of the cloud")]
        public string Center { get; set; }

        [Name("c", "color")]
        [Description("Color of words")]
        public string Color { get; set; }
    }
}