using System;
using System.Collections.Generic;
using TagsCloudVisualization.Infrastructure;
using CommandLine;

namespace TagsCloudVisualization
{
    public class Options
    {
        [Value(0, Required = true, HelpText = "Name of file with words to be layouted")]
        public string WordsFilename { get; set; }

        [Option('o', "output", Default = "examples/text.png", HelpText = "Output filename")]
        public string OutFilename { get; set; }
        
        [Option('f', Default = "Times New Roman", HelpText = "Font family for words visualizing")]
        public string FontFamily { get; set; }

        [Option('t', Default = 0, HelpText = "Font style number for words visualizing")]
        public int FontStyle { get; set; }

        [Option('p', Min = 2, Max = 2, Default = new []{0, 0}, HelpText = "Spiral central point")]
        public IEnumerable<int> CentralPoint { get; set; }
        
        [Option('a', Default = 0, HelpText = "Spiral start angle")]
        public int Angle { get; set; }
        
        [Option('s', Default = 0.0005, HelpText = "Spiral step")]
        public double Step { get; set; }
        
        [Option('b', "--bg", Default = "DimGray", HelpText = "Background color")]
        public string BackgroundColor { get; set; }
        
        [Option('g', "--fg", Default = "FloralWhite", HelpText = "Foreground color")]
        public string ForegroundColor { get; set; }
        
        [Option('z', Min = 2, Max = 2, Default = new []{800, 800}, HelpText = "Picture size")]
        public IEnumerable<int> Size { get; set; }

        [Option('m', Default = 100f, HelpText = "Max font size")]
        public float MaxFontSize { get; set; }

        [Option('d', Min = 2, Max = 2,Default = new[] {"Dictionaries/ru.aff", "Dictionaries/ru.dic"}, HelpText = "Hunspell dictionary files")]
        public IEnumerable<string> DictFileNames { get; set; }

        [Option('w', Default = "examples/boring_words.txt", HelpText = "Boring words filename")]
        public string BoringWordsFile { get; set; }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"exit with code {Parser.Default.ParseArguments<Options>(args).MapResult(RunAndReturnExitCode, errs => 1)}");
        }

        private static int RunAndReturnExitCode(Options options)
        {
            new DIBuilder(options).Resolve().Save(options.OutFilename);
            return 0;
        }
    }
}
