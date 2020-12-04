using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class StorageCommands
    {
        [Required, Name("in", "myText"), Description("The path to text file")]
        public string PathToCustomText { get; set; }

        [Required, Name("out", "outImage"), Description("The path to save image without format")]
        public string PathToSave { get; set; }

        [Name("if", "imageFormat"), DefaultValue("jpeg"),
         Description("Image format. Supported formats: \"bmp\", \"jpeg\", \"png\", \"gif\", \"tiff\"")]
        public string ImageFormat { get; set; }
    }
}