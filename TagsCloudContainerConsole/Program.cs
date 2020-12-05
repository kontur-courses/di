using System;
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
            
            [Option(Default = 16)]
            public int FontSize { get; set; }

            [Option(Default = ScalingMethods.Linear, HelpText = "Scaling method (Linear | Sqrt | LerpTotal | LerpMax)")]
            public ScalingMethods Scaling { get; set; }
            
            [Option(Default = 1f, HelpText = "Min scaling value for LerpTotal/LerpMax scaling methods")]
            public float ScalingLerpMin { get; set; }
            
            [Option(Default = 6f, HelpText = "Max scaling value for LerpTotal/LerpMax scaling methods")]
            public float ScalingLerpMax { get; set; }
            
            [Option(Default = ColoringMethods.LerpMax, HelpText = "Coloring method (LerpTotal | LerpMax)")]
            public ColoringMethods Coloring { get; set; }
            
            [Option(HelpText = "Color used as first in lerp function in coloring method")]
            public Color? ColoringFrom { get; set; }
            
            [Option(HelpText = "Color used as second in lerp function in coloring method")]
            public Color? ColoringTo { get; set; }

            [Option(Default = true)]
            public bool AutoSize { get; set; }
        }
        
        public enum ScalingMethods
        {
            Linear,
            Sqrt,
            LerpTotal,
            LerpMax,
        }

        public enum ColoringMethods
        {
            LerpTotal,
            LerpMax
        }
        
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                var coloringFrom = options.ColoringFrom ?? Color.FromArgb(0, 255, 128);
                var coloringTo = options.ColoringTo ?? Color.FromArgb(255, 0, 128);
                
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

                var scalingMethods =
                    new Dictionary<ScalingMethods, Func<WordRendererToImage.SizingInfo, LayoutedWord, float>>
                    {
                        [ScalingMethods.Linear] = (info, word) => word.Count,
                        [ScalingMethods.Sqrt] = (info, word) => (float) Math.Sqrt(word.Count),
                        [ScalingMethods.LerpTotal] = (info, word) =>
                        {
                            var min = options.ScalingLerpMin;
                            var ammount = options.ScalingLerpMax - min;
                            return min + ammount * word.Count / info.TotalWordsCount;
                        },
                        [ScalingMethods.LerpMax] = (info, word) =>
                        {
                            var min = options.ScalingLerpMin;
                            var ammount = options.ScalingLerpMax - min;
                            return min + ammount * (word.Count - info.MinWordCount) / (info.MaxWordCount - info.MinWordCount);
                        }
                    };

                var coloringMethods =
                    new Dictionary<ColoringMethods, Func<WordRendererToImage.RenderingInfo, LayoutedWord, Color>>
                    {
                        [ColoringMethods.LerpTotal] = (info, word)
                            => Lerp(coloringFrom, coloringTo, word.Count / (float) info.TotalWordsCount),
                        [ColoringMethods.LerpMax] = (info, word)
                            => Lerp(coloringFrom, coloringTo, 
                                (word.Count - info.MinWordCount) / (float) (info.MaxWordCount - info.MinWordCount))
                    };

                var scaling = scalingMethods[options.Scaling];
                var coloring = coloringMethods[options.Coloring];
                
                var renderer = new WordRendererToImage()
                    .WithFont(new Font(options.Font, options.FontSize, GraphicsUnit.Pixel))
                    .WithScale(scaling)
                    .WithColor(coloring);
                renderer.AutoSize = options.AutoSize;
                if (!options.AutoSize) renderer.Output = new Bitmap(options.Width, options.Height);
                container.Rendering(renderer);
                container.Render();
                renderer.Output.Save(options.OutputFile, ImageFormat.Png);
            });
        }

        public static HashSet<string> ReadWordsFromFile(string fileName)
        {
            var text = File.ReadAllText(fileName);
            return Regex.Matches(text, @"\b\w+\b").Cast<Match>().Select(m => m.Value).ToHashSet();
        }

        public static int Lerp(int from, int to, float progress) => (int) (from + (to - from) * progress);

        public static Color Lerp(Color from, Color to, float progress)
            => Color.FromArgb(
                Lerp(from.A, to.A, progress),
                Lerp(from.R, to.R, progress),
                Lerp(from.G, to.G, progress),
                Lerp(from.B, to.B, progress));
    }
}