using CommandLine;

namespace TagsCloudContainer.ProjectSettings
{
    public class ConsoleClientOptions
    {
        [Option('h', "height", Required = true, HelpText = "Set height of image")]
        public int Height { get; set; }

        [Option('w', "width", Required = true, HelpText = "Set width of image")]
        public int Width { get; set; }

        [Option('f', "font", Required = true, HelpText = "Font of text")]
        public string Font { get; set; }

        [Option('t', "text_path", Required = true, HelpText = "Source path of words")]
        public string Source { get; set; }

        [Option('p', "picture_path", Required = true, HelpText = "Destination path of tags cloud")]
        public string Destination { get; set; }

        [Option('c', "text_color", Required = true, HelpText = "Color of text")]
        public string Color { get; set; }
    }

}