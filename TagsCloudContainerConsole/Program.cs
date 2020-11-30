using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using TagsCloudContainer;

namespace TagsCloudContainerConsole
{
    internal class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Input files or strings")]
            public IEnumerable<string> Inputs { get; set; }
            
            [Option('e', "excluding", Default = new [] {"excluding.txt", "preps.txt", "pronouns.txt"},
                HelpText = "Files of words that will be excluded from cloud")]
            public IEnumerable<string> Excluding { get; set; }
            
            [Option('o', "output", Default = "out.png", HelpText = "Output image file")]
            public string OutputFile { get; set; }

            [Option('w', "width", Default = 800, HelpText = "Output image width")]
            public int Width { get; set; }
            
            [Option('h', "height", Default = 600, HelpText = "Output image height")]
            public int Height { get; set; }
            
            [Option(Default = "Times New Roman")]
            public string Font { get; set; }
            
            [Option(Default = 32)]
            public int FontSize { get; set; }
        }
        
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                var container = new TagsCloudContainer.TagsCloudContainer();
                
                foreach (var inputFile in options.Inputs)
                {
                    if (File.Exists(inputFile))
                        container.AddFromFile(inputFile);
                    else container.AddFromText(inputFile);
                }
                
                foreach (var excluded in options.Excluding)
                {
                    if (File.Exists(excluded))
                        container.Excluding(ReadWordsFromFile(excluded));
                }
                
                var output = new Bitmap(options.Width, options.Height);
                container.Rendering(new WordRendererToImage(output)
                    .WithFont(new Font(options.Font, options.FontSize, GraphicsUnit.Pixel)));
                container.Render();
                output.Save(options.OutputFile, ImageFormat.Png);
            });
        }

        public static HashSet<string> ReadWordsFromFile(string fileName)
        {
            var text = File.ReadAllText(fileName);
            return Regex.Matches(text, @"\b\w+\b").Cast<Match>().Select(m => m.Value).ToHashSet();
        }
    }
}