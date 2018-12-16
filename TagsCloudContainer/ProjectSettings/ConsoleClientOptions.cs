using CommandLine;

namespace TagsCloudContainer.ProjectSettings
{
    public class ConsoleClientOptions
    {
        [Option('h', "height", Required = true, HelpText = "Height of image")]
        public int Height { get; set; }

        [Option('w', "width", Required = true, HelpText = "Width of image")]
        public int Width { get; set; }

        [Option('f', "font_famliy", Required = false, HelpText = "FontFamily of text", Default = "Arial")]
        public string FontFamily { get; set; }

        [Option('t', "text_path", Required = true, HelpText = "Source path of words")]
        public string Source { get; set; }

        [Option('p', "picture_path", Required = true, HelpText = "Destination path of tags cloud")]
        public string Destination { get; set; }

        [Option('c', "text_color", Required = false, HelpText = "Color of text", Default = "Blue")]
        public string Color { get; set; }

        [Option('s', "font_size", Required = false, HelpText = "base font size", Default = 30)]
        public int BaseFontSize { get; set; }
    }

}