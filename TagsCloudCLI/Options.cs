using CommandLine;

namespace TagsCloudCLI
{
    internal class Options
    {
        [Option("directory", HelpText = "Path to file with words", Default = "ImageExamples")]
        public string Directory { get; set; }

        [Option("image_name", HelpText = "Image name", Default = "example.png")]
        public string ImageName { get; set; }

        [Option("font_family", HelpText = "Family name of the words", Default = "Arial")]
        public string FontFamilyName { get; set; }

        [Option("color", HelpText = "Words color", Default = "Firebrick")]
        public string TagColor { get; set; }

        [Option("file_with_words", HelpText = "Words color", Default = "text.txt")]
        public string FileWithWords { get; set; }

        [Option("font_size", HelpText = "Max font size", Default = 100)]
        public int MaxFontSize { get; set; }
    }
}