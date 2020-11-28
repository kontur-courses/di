using CommandLine;

namespace TagCloud.Visualizer.Console
{
    public class Options
    {
        [Option("fontSize", Default = 12f)]
        public float FontSize { get; set; }
        
        [Option("fontName", Default = "Times New Roman")]
        public string FontName { get; set; }
        
        [Option("height", Default = 1000)]
        public int ImageHeight { get; set; }
        
        [Option("width", Default = 1000)]
        public int ImageWidth { get; set; }
        
        [Option("color", Default = "Black")]
        public string ColorName { get; set; }
    }
}