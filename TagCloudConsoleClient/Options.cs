using System.Drawing;
using CommandLine;

namespace TagCloudConsoleClient
{
    public  class Options
    {
        [Value(0, MetaName = "source", HelpText = "Path to source text file.", Required = true)]
        public string SourcePath { get; set; }
        
        [Value(1, MetaName = "result", HelpText = "Path to result image.", Required = true)]
        public string ResultPath { get; set; }
        
        [Value(2, MetaName = "width", HelpText = "Width of result image.", Required = true)]
        public int Width { get; set; }
        
        [Value(3, MetaName = "height", HelpText = "Height of result image.", Required = true)]
        public int Height { get; set; }

        [Option('f', "font", HelpText = "Set font. If unknown sets to default.")]
        public string Font { get; set; } = SystemFonts.DefaultFont.Name;

        [Option('c', "count", HelpText = "Set maximum words to render.")]
        public int MaxCount { get; set; } = 100;

        [Option('m', "manhattan", HelpText = "Use manhattan metric in algorithm.")]
        public bool Manhattan { get; set; }
        
        [Option('o', "open", HelpText = "Opens result at the end.")]
        public bool OpenResult { get; set; }
        
        [Option('s', "shape", HelpText = "OrderOfTags: N(NonOrderer), S(Sorted) or M(Mixed)")]
        public string Order { get; set; }
    }
}