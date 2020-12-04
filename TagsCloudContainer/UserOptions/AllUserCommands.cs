using System.Drawing;
using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class AllUserCommands : StorageCommands
    {
        [Name("f", "font"), DefaultValue(new[] {"arial", "7"}),
         Description("Text font with min size, space separated font name and size")]
        public string[] Font { get; set; }

        [Name("s", "size"), DefaultValue(new[] {0, 0}),
         Description("Image size with int parameters, space separated width and height")]
        public int[] ImageSize { get; set; }

        [Name("tc", "textColor"), DefaultValue(KnownColor.Red), Description("Text color")]
        public KnownColor TextColor { get; set; }

        [Name("bgc", "backColor"), DefaultValue(KnownColor.Black), Description("Background color")]
        public KnownColor BackgroundColor { get; set; }

        [Name("bw", "boring"), DefaultValue(new string[0]), Description("Boring words, space separated between words")]
        public string[] BoringWords { get; set; }
    }
}