using CommandLine;

namespace TagsCloudContainer
{
    public class CommandLineOptions
    {
        [Option("-width", Required = false, HelpText = "Set output image width.", Default = 500)]
        public int Width { get; set; }
        
        [Option("-height", Required = false, HelpText = "Set output image height.", Default = 500)]
        public int Height { get; set; }
        
        [Option("-font", Required = false, HelpText = "Set output text font.", Default = "Times New Roman")]
        public string Font { get; set; }
        
        [Option("-file", Required = true, HelpText = "Set input file path.")]
        public string FilePath { get; set; }
    }
}