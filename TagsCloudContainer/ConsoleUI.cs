using System.Drawing;
using CommandLine;

namespace TagsCloudContainer
{
    public class ConsoleUi
    {
        [Option('f', "font", Required = false, Default = "Arial", HelpText = "Font name")]
        public string FontName { get; set; }

        [Option("backgroundcolor", Required = false, Default = "Gray", HelpText = "Background color")]
        public string BackGroungColor { get; set; }

        [Option("brushcolor", Required = false, Default = "Blue", HelpText = "Brush color")]
        public string BrushColor { get; set; }

        [Option('s', "savepath", Required = false, Default = "../visualization",
            HelpText = "Path to directory to save image")]
        public string PathToSave { get; set; }

        [Option('o', "openpath", Required = false,
            Default = "../testfile.txt",
            HelpText = "Path to file with words")]
        public string PathToOpen { get; set; }

        [Option('w', "width", Required = false, Default = 1000, HelpText = "Width of canvas")]
        public int CanvasWidth { get; set; }

        [Option('h', "height", Required = false, Default = 1000, HelpText = "Height of canvas")]
        public int CanvasHeight { get; set; }

        [Option('b', "border", Required = false, Default = 150, HelpText = "Borders of canvas")]
        public int CanvasBorder { get; set; }

        [Option('r', "radius", Required = false, Default = 0.1, HelpText = "Radius offset")]
        public double RadiusOffset { get; set; }

        [Option('a', "angle", Required = false, Default = 0.1, HelpText = "Angle offset")]
        public double AngleOffset { get; set; }
    }
}