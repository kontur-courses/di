using CommandLine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.Client;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintConfigs;
using TagsCloudContainer.TextParsers;

namespace CLI
{
    public class Client : IClient
    {
        private readonly Dictionary<string, Func<string, string>> wordModifiers;
        private readonly string[] args;
        public IUserConfig UserConfig { get; }

        public Client(string[] args)
        {
            this.args = args;
            wordModifiers = new Dictionary<string, Func<string, string>>
            {
                {"lower", s => s.ToLower()},
                {"trim", s => s.Trim()}
            };
            UserConfig = ParseArguments();
        }

        private CommandLineConfig ParseArguments()
        {
            var userConfig = new CommandLineConfig();
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options => BuildConfigFrom(options, userConfig))
                .WithNotParsed(errs => throw new Exception(
                    $"Failed with errors:\n{string.Join("\n", errs)}"));

            return userConfig;
        }

        private void BuildConfigFrom(Options options, IUserConfig config)
        {
            config.InputFile = options.Input;
            config.OutputFile = options.Output;
            config.Tags = options.Tags.ToArray();
            config.ImageSize = TryGetImageSize(options);
            config.ImageCenter = GetImageCenter(config.ImageSize);
            CheckFontParams(options);
            config.TagsFontName = options.FontName;
            config.TagsFontSize = options.FontSize;
            config.TagsColors = TryGetTagsColors(options);
            config.Spiral = TryGetSpiral(options, config.ImageCenter);
            config.ImageFormat = TryGetImageFormat(options);
            config.TextParser = TryGetTextParser(options);
        }

        private ITextParser TryGetTextParser(Options options)
        {
            var sourceReader = TryGetSourceReader(options);
            var wordHandlers = GetWordHandlers(options);
            var wordGroper = GetWordGrouper();
            return new TextParser(sourceReader, wordHandlers, wordGroper);
        }

        private ISourceReader TryGetSourceReader(Options options)
        {
            ISourceReader sourceReader;
            if (options.Input != null)
            {
                if (options.InputFileFormat != "txt")
                    throw new ArgumentException("Unknown text format is given!");
                sourceReader = new TxtFileReader(options.Input);
            }
            else if (options.Tags != null)
                sourceReader = new TagsReader(options.Tags);
            else throw new ArgumentNullException("Both file and tags cannot be null!");
            return sourceReader;
        }

        private static Action<string, Dictionary<string, int>> GetWordGrouper()
        {
            return (word, dict) =>
            {
                if (word == "") return;
                if (!dict.TryGetValue(word, out _))
                    dict.Add(word, 0);
                dict[word]++;
            };
        }

        private List<Func<string, string>> GetWordHandlers(Options options)
        {
            var handlersList = new List<Func<string, string>>();
            foreach (var funcName in options.Modifications)
                if (wordModifiers.ContainsKey(funcName))
                    handlersList.Add(wordModifiers[funcName]);

            handlersList.Add(GetWordsExcludingFunc(options));

            return handlersList;
        }

        private Func<string, string> GetWordsExcludingFunc(Options options)
        {
            return s =>
            {
                var excluded = options.ExcludedWords.ToHashSet();
                var boringWords = new BoringWords().GetWords();
                if (excluded.Contains(s) || boringWords.Contains(s)) return "";
                return s;
            };
        }

        private ImageFormat TryGetImageFormat(Options options)
        {
            var formatName = options.OutputFileFormat.ToLower();
            switch (formatName)
            {
                case "png": return ImageFormat.Png;
                case "bmp": return ImageFormat.Bmp;
                case "jpeg": return ImageFormat.Jpeg;
                default: throw new ArgumentException("Uknown output image format!");
            }
        }

        private ISpiral TryGetSpiral(Options options, Point imageCenter)
        {
            var spiralName = options.Spiral.ToLower();
            switch (spiralName)
            {
                case "log": return new LogarithmSpiral(imageCenter);
                case "sqr": return new SquareSpiral(imageCenter);
                case "rnd": return new RandomSpiral(imageCenter);
                default: throw new ArgumentException("Unknown spiral kind!");
            }
        }

        private void CheckFontParams(Options options)
        {
            var tagsFontName = options.FontName;
            var font = new Font(tagsFontName, options.FontSize);
            if (font.Name != tagsFontName)
                throw new ArgumentException("Font name is incorrect!");
            font.Dispose();
        }

        private IColorScheme TryGetTagsColors(Options options)
        {
            switch (options.Color)
            {
                case 0: return new BlackWhiteScheme();
                case 1: return new CamouflageScheme();
                case 2: return new CyberpunkScheme();
                default: throw new ArgumentException("Unknow color scheme number!");
            }
        }

        private Size TryGetImageSize(Options options)
        {
            var width = options.Width;
            var height = options.Height;
            if (width <= 0 || height <= 0)
                throw new ArgumentException("One or both sides are less or equal zero");
            return new Size(width, height);
        }

        private Point GetImageCenter(Size imageSize)
            => new Point(imageSize.Width / 2, imageSize.Height / 2);
    }
}
