using CommandLine;

namespace TagsCloudBuilder
{
    public class Options
    {
        [Option('i', "input", Default = "text.txt",
            HelpText = "Filename with input data.")]
        public string InputFilename { get; set; }

        [Option('d', "debug", Required = false, Default = false,
            HelpText = "Draw area around every word.")]
        public bool Debug { get; set; }

        [Option('s', "max font size", Required = false, Default = 50,
            HelpText = "Set the max size of the words in px. Can't be less then 10px.")]
        public int MaxFontSize { get; set; }

        [Option('f', "font", Required = false, Default = "Arial",
            HelpText = "Set the font for text.")]
        public string FontFamily { get; set; }

        [Option('b', "boring words", Required = false, Default = "boring.txt",
            HelpText = "Filename with ignored words.")]
        public string BannedWordsFilename { get; set; }

        [Option('s', "canvas size", Required = false, Default = new[] { 2000, 2000 },
            HelpText = "Set the canvas size of output file.")]
        public int[] CanvasSize { get; set; }

        [Option('o', "output filename", Required = false, Default = "sample.png",
            HelpText = "Set the output file name.")]
        public string OutputFilename { get; set; }

        [Option('c', "center point", Required = false, Default = new[] { 750, 1000 },
            HelpText = "Set the center of clouds.")]
        public int[] CenterPoint { get; set; }

        [Option('r', "radius step", Required = false, Default = 0.00001,
            HelpText = "Set the radius step.")]
        public double RadiusStep { get; set; }

        [Option('a', "angle step", Required = false, Default = 0.01,
            HelpText = "Set the angle step.")]
        public double AngleStep { get; set; }

        [Option('l', "words length", Required = false, Default = new[] { 5, int.MaxValue },
            HelpText = "Set the bounds of words length")]
        public int[] WordsLength { get; set; }

        [Option('e', "output file extension", Required = false, Default = "png",
            HelpText = "Set the output file extension.")]
        public string OutputFileExtension { get; set; }

        [Option('c', "color algorithm", Required = false, Default = "random",
            HelpText = "Set the color algorithm chooser.")]
        public string ColorAlgorithmName { get; set; }
    }
}