using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class ColorsCommands : RequiredUserCommands
    {
        [Required, Name("tc", "textColor"), Description("Text color")]
        public string TextColor { get; set; }

        [Required, Name("bgc", "backColor"), Description("Background color")]
        public string BackgroundColor { get; set; }
    }
}