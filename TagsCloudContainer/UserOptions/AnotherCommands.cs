using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class AnotherCommands : RequiredUserCommands
    {
        [Required, Name("f", "font"), Description("Text font, underscores separated font name and size")]
        public string Font { get; set; }

        [Required, Name("s", "size"), Description("Image size with int parameters, underscores separated width and height")]
        public string ImageSize { get; set; }
    }
}