using System;
using CommandLine;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Clients.CLI
{
    public class Options
    {
        [Option('w', "words", Required = true, HelpText = "File with words")]
        public string WordsPath { get; set; }

        [Option('b', "boring", Required = true, HelpText = "File with boring words")]
        public string BoringWordsPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "The file name of the generated tag cloud")]
        public string ImagePath { get; set; }

        [Option("aff", Required = false, HelpText = "The path to the aff file")]
        public string AffPath { get; set; }

        [Option("dic", Required = false, HelpText = "The path to the dic file")]
        public string DicPath { get; set; }

        [Option("distance", Required = false, Default = 1, HelpText = "The distance between the turns of the spiral")]
        public double Distance { get; set; }

        [Option("delta", Required = false, Default = Math.PI / 180, HelpText = "The offset angle of the spiral")]
        public double Delta { get; set; }

        [Option("font", Required = false, Default = "Courier New", HelpText = "The font of tags")]
        public string Font { get; set; }

        [Option("factor", Required = false, Default = 100f, HelpText = "Tag size factor")]
        public float SizeFactor { get; set; }

        [Option("painter", Required = false, Default = SteppedColorPainter.Name, HelpText = "Tag coloring algorithm")]
        public string Painter { get; set; }
        
        [Option("layouter", Required = false, Default = CircularCloudLayouter.Name, HelpText = "Cloud layout algorithm")]
        public string Layouter { get; set; }

        [Option("text", Required = false, Default = "Black", HelpText = "Text color name")]
        public string TextColor { get; set; }

        [Option("fill", Required = false, Default = "Transparent", HelpText = "Fill color name")]
        public string FillColor { get; set; }

        [Option("border", Required = false, Default = "Transparent", HelpText = "Border color name")]
        public string BorderColor { get; set; }

        [Option("primary", Required = false, Default = "LightYellow", HelpText = "Primary text color")]
        public string PrimaryColor { get; set; }

        [Option("majority", Required = false, Default = "OrangeRed", HelpText = "Majority text color")]
        public string MajorityColor { get; set; }

        [Option("minority", Required = false, Default = "Chocolate", HelpText = "Minority text color")]
        public string MinorityColor { get; set; }

        [Option("background", Required = false, Default = "Transparent", HelpText = "Background color name")]
        public string BackgroundColor { get; set; }

        [Option("width", Required = false, HelpText = "Image width")]
        public int? Width { get; set; }

        [Option("height", Required = false, HelpText = "Image height")]
        public int? Height { get; set; }
    }
}