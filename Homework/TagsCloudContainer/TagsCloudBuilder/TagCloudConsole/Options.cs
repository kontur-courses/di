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

        [Option('x', "maxFontSize", Required = false, Default = 50,
            HelpText = "Set the max size of the words in px. Can't be less then 10px.")]
        public int MaxFontSize { get; set; }

        [Option('f', "font", Required = false, Default = "Arial",
            HelpText = "Set the font for text.")]
        public string FontFamily { get; set; }

        [Option('b', "boringWords", Required = false, Default = "boring.txt",
            HelpText = "Filename with ignored words.")]
        public string BannedWordsFilename { get; set; }

        [Option('s', "canvasSize", Required = false, Default = new[] { 2000, 2000 },
            HelpText = "Set the canvas size of output file.")]
        public int[] CanvasSize { get; set; }

        [Option('o', "outputFilename", Required = false, Default = "sample.png",
            HelpText = "Set the output file name.")]
        public string OutputFilename { get; set; }

        [Option('p', "centerPoint", Required = false, Default = new[] { 750, 1000 },
            HelpText = "Set the center of clouds.")]
        public int[] CenterPoint { get; set; }

        [Option('r', "radiusStep", Required = false, Default = 0.00001,
            HelpText = "Set the radius step.")]
        public double RadiusStep { get; set; }

        [Option('a', "angleStep", Required = false, Default = 0.01,
            HelpText = "Set the angle step.")]
        public double AngleStep { get; set; }

        [Option('l', "wordsLength", Required = false, Default = new[] { 5, int.MaxValue },
            HelpText = "Set the bounds of words length")]
        public int[] WordsLength { get; set; }

        [Option('e', "outputFile extension", Required = false, Default = "png",
            HelpText = "Set the output file extension.")]
        public string OutputFileExtension { get; set; }

        [Option('c', "colorAlgorithm", Required = false, Default = "random",
            HelpText = "Set the color algorithm chooser.")]
        public string ColorAlgorithmName { get; set; }
    }
}