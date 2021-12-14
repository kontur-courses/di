using CommandLine;

namespace TagsCloudCLI
{
    internal class Options
    {
        [Option('d', "directory", HelpText = "Path to save image", Required = true )]
        public string Directory { get; set; }

        [Option('i', "image_name", HelpText = "Image name", Default = "example.png")]
        public string ImageName { get; set; }

        [Option('f',"font_family", HelpText = "Family name of the words", Default = "Arial")]
        public string FontFamilyName { get; set; }

        [Option('c',"color", HelpText = "Words color", Default = "Blue")]
        public string TagColor { get; set; }

        [Option('w', "file_with_words", HelpText = "File with words", Required = true)]
        public string FileWithWords { get; set; }

        [Option('s',"font_size", HelpText = "Max font size", Default = 100)]
        public int MaxFontSize { get; set; }

        [Option('b',"boring_words", HelpText = "Path to file with boring words", Default = "boring.txt")]
        public string PathToBoringWords { get; set; }
    }
}