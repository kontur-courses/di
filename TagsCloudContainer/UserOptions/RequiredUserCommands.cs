using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class RequiredUserCommands
    {
        [Required, Name("in", "myText"), Description("The path to text file")]
        public string PathToCustomText { get; set; }

        [Required, Name("out", "outImage"), Description("The path to save image")]
        public string PathToSave { get; set; }
    }
}