using CommandLine;

namespace TagsCloudVisualization
{
    public class Options
    {
        [Option('i', "input_path", Required = false, HelpText = "Specify the input path to the file",
            Default = @"..\..\..\Text.txt")]
        public string PathToInputFile { get; set; }
        
        [Option('o', "output_path", Required = false, HelpText = "Specify the output path to the file",
            Default = @"..\..\..\Images\")]
        public string PathToOutputFile { get; set; }

        [Option('w',"width", Required = false, HelpText = "Specify image width", Default = 1400)]
        public int Width { get; set; }

        [Option('h', "height", Required = false, HelpText = "Specify image height", Default = 1200)]
        public int Height { get; set; }

        [Option('f', "font", Required = false, HelpText = "Specify text fontFamily", Default = "Arial")]
        public string FontFamily { get; set; }

        [Option("output_name", Required = false, HelpText = "Specify output file name", Default = "TagCloud")]
        public string OutputFileName { get; set; }

        [Option("ext", Required = false, HelpText = "Specify output file extension.", Default = ".png")]
        public string OutputFileExtension { get; set; }

        [Option("font_color", Required = false, HelpText = "Specify font color. Example: --font_color 10,10,10,10", Default = "random")]
        public string FontColor { get; set; }

        [Option("angle_step", Required = false, HelpText = "Specify angle_step.", Default = 0.3)]
        public double AngleStep { get; set; }

        [Option("exclude", Required = false, HelpText = "Specify excluded words. " +
                                                        "Example: --exclude Привет, мы, скучные, слова", Default = "")]
        public string ExcludedWords { get; set; }

    }
}
