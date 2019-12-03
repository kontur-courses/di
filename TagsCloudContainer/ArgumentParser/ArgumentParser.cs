using CommandLine;

namespace TagsCloudContainer
{
    public class ArgumentParser : IArgumentParser
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
        public Setting ParseArgument(string[] args)
        {
            var setting = default(Setting);
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                   setting = new Setting(o.Filename, o.Font, o.Size, o.Width, o.Height, o.Color);
                   
                });
            return setting;
        }
    }
}