using CommandLine;
using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class Options
    {
        [Option('w', "width", Default = 1920)]
        public int Width { get; set; }

        [Option('h', "height", Default = 1080)]
        public int Height { get; set; }

        [Option('o', "output", Default = "output.png")]
        public string OutputFile { get; set; }

        [Option('i', "input", Default = "input.txt")]
        public string InputFile { get; set; }

        [Option('c', "count", Default = -1)]
        public int Count { get; set; }

        [Option('a', "astep", Default = Math.PI / 180)]
        public double StepAngle { get; set; }

        [Option('x', "xstep", Default = 2)]
        public int StepX { get; set; }

        [Option('y', "ystep", Default = 1)]
        public int StepY { get; set; }

        [Option('f', "fgcolor", Default = "black")]
        public string ForegroundColor { get; set; }

        [Option('b', "bgcolor", Default = "white")]
        public string BackgroundColor { get; set; }

        [Option("minf", Default = 10)]
        public int MinFontSize { get; set; }

        [Option("maxf", Default = 50)]
        public int MaxFontSize { get; set; }

        [Option("pspeech", Separator = ',', Default = new[] { "A", "V", "S" })]
        public IEnumerable<string> PartSpeeches { get; set; }
    }
}
