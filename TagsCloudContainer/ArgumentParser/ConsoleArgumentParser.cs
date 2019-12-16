using System;
using CommandLine;
using TagsCloudContainer.Infrastructure.Common;

namespace TagsCloudContainer
{
    public class ConsoleArgumentParser : IArgumentParser
    {
        class Options
        {
            [Option('f', "file", Required = true, HelpText = "File with words")]
            public string Filename { get; set; }

            [Option('o', "font", Required = false, HelpText = "Name of font", Default = "Arial")]
            public string Font { get; set; }

            [Option('h', "height", Required = false, HelpText = "Height of image", Default = 480)]
            public int Height { get; set; }

            [Option('w', "width", Required = false, HelpText = "Width of image", Default = 640)]
            public int Width { get; set; }

            [Option('c', "color", Required = false,
                HelpText = "Color of word('random' if you want different color of words)", Default = "Black")]
            public string Color { get; set; }

            [Option('b', "backgroundColor", Required = false, HelpText = "BackGround color", Default = "White")]
            public string BackColor { get; set; }

            [Option('a', "algorithm", Required = false, HelpText = "True if you want another picture", Default = false)]
            public bool Cenrings { get; set; }

            [Option('F', "format", Required = false, HelpText = "jpeg, png, bmp and other (Default: png)",
                Default = "png")]
            public string Format { get; set; }

            [Option('n', "name", Required = true, HelpText = "Name for saving image")]
            public string Name { get; set; }
        }

        public WordSetting GetWordSetting(string[] args)
        {
            var setting = default(WordSetting);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => { setting = new WordSetting(o.Font, o.Color); });
            return setting;
        }

        public ImageSetting GetImageSetting(string[] args)
        {
            var setting = default(ImageSetting);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    setting = new ImageSetting(o.Height, o.Width, o.BackColor, o.Format, o.Name);
                });
            return setting;
        }

        public AlgorithmsSettings GetAlgorithmsSettings(string[] args)
        {
            var setting = default(AlgorithmsSettings);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => { setting = new AlgorithmsSettings(o.Cenrings); });
            return setting;
        }

        public string GetPath(string[] args)
        {
            var path = default(string);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => { path = o.Filename; });
            return path;
        }
    }
}