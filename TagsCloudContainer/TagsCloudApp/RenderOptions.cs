using System;
using System.Drawing;
using CommandLine;

namespace TagsCloudApp
{
    [Verb("render")]
    public class RenderOptions
    {
        [Option('i', "input", Default = "input.txt")]
        public string InputPath { get; set; }

        [Option('o', "output", Default = "output.png")]
        public string OutputPath { get; set; }

        [Option("fontFamily", HelpText = "(Default: Courier New)")]
        public FontFamily FontFamily { get; set; } = new("Courier New");

        [Option("maxFont", Default = 32)]
        public int MaxFontSize { get; set; }

        [Option("minFont", Default = 8)]
        public int MinFontSize { get; set; }

        [Option('s', "size", HelpText = "(Default: auto adjust) (Example: 10, 10) ")]
        public Size? ImageSize { get; set; } = null;

        [Option("scale", Default = 1.0f)]
        public float ImageScale { get; set; }

        [Option("background", HelpText = "(Default: Transparent)")]
        public Color BackgroundColor { get; set; } = Color.Transparent;


        public static void HandleCommand(RenderOptions options)
        {
            try
            {
                new RenderCommand(options).Render();
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}