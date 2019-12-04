using System;
using CommandLine;

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
            
            [Option('s', "size", Required = false, HelpText = "Size of letter", Default = 10)]
            public int Size { get; set; }
            
            [Option('c', "color", Required = false, HelpText = "Color of word", Default = "Black")]
            public string Color { get; set; }
        }
        public WordSetting GetWordSetting(string[] args)
        {
            var setting = default(WordSetting);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                   setting = new WordSetting(o.Font, o.Size, o.Color);
                   
                });
            return setting;
        }

        public ImageSetting GetImageSetting(string[] args)
        {
            var setting = default(ImageSetting);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    setting = new ImageSetting(o.Height, o.Width);
                   
                });
            return setting;
        }

        public string GetPath(string[] args)
        {
            var path = default(string);
            Parser.Default.ParseArguments<Options>(args).WithNotParsed<Options>(errors => throw new ArgumentException())
                .WithParsed<Options>(o =>
                {
                    path = o.Filename;
                });
            return path;
        }
    }
}