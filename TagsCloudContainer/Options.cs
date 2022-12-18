using System;
using System.Collections.Generic;
using CommandLine;

namespace TagsCloudContainer
{
    public class Options
    {
        [Option('w', "width", Required = true)]
        public int Width { get; set; }

        [Option('h', "height", Required = true)]
        public int Height { get; set; }

        [Option('x', "centerX", Required = true)]
        public int CenterX { get; set; }

        [Option('y', "centerY", Required = true)]
        public int CenterY { get; set; }

        [Option('c', "count", Required = true)]
        public int Count { get; set; }

        [Option('o', "output", Default = "output.png")]
        public string OutputFile { get; set; }

        [Option('i', "input", Default = "input.txt")]
        public string InputFile { get; set; }

        [Option("astep", Default = Math.PI / 180)]
        public double StepAngle { get; set; }

        [Option("xstep", Default = 2)] public int StepX { get; set; }

        [Option("ystep", Default = 1)] public int StepY { get; set; }

        [Option("fgcolor", Default = "black")]
        public string ForegroundColor { get; set; }

        [Option("bgcolor", Default = "white")] 
        public string BackgroundColor { get; set; }

        [Option("family", Default = "Arial")] 
        public string FontFamily { get; set; }

        [Option("minf", Default = 10)] 
        public int MinFontSize { get; set; }

        [Option("maxf", Default = 50)] 
        public int MaxFontSize { get; set; }

        [Option("pspeech", Separator = ',', Default = new[] { "A", "V", "S" })]
        public IEnumerable<string> PartSpeeches { get; set; }
    }
}