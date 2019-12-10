using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudLibrary;
using TagsCloudLibrary.Colorers;
using TagsCloudLibrary.Layouters;
using TagsCloudLibrary.MyStem;
using TagsCloudLibrary.Preprocessors;
using TagsCloudLibrary.Readers;
using TagsCloudLibrary.WordsExtractor;
using TagsCloudLibrary.Writers;

namespace TagsCloudCLI
{
    class Program
    {
        private class Options
        {
            [Option('r', "read", Required = true, HelpText = "Input file to read")]
            public string InputFile { get; set; }
            
            [Option('w', "write", Required = true, HelpText = "Output file name")]
            public string OutputFile { get; set; }

            [Option('t', "text", Required = false, Default = false, HelpText = "Is input file containing text and not lines with words")]
            public bool IsText { get; set; }

            [Option('o', "only", Required = false, Default = null, HelpText = "Select only specified parts of speech")]
            public IEnumerable<string> PartsOfSpeechWhiteList { get; set; }

            [Option("width", Required = false, Default = 10000, HelpText = "Width of the image")]
            public int ImageWidth { get; set; }

            [Option("height", Required = false, Default = 10000, HelpText = "Width of the image")]
            public int ImageHeight { get; set; }

            [Option('f', "format", Required = false, Default = "png", HelpText = "Output image format")]
            public string OutputFormat { get; set; }

            [Option('c', "color", Required = false, Default = "black", HelpText = "Coloring algorithm or constant color")]
            public string Color { get; set; }
            
            [Option("font", Required = false, Default = "Arial", HelpText = "Font family for text")]
            public string Font { get; set; }
        }

        static void Main(string[] args)
        {
            Options options = null;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => { options = o; });

            if (options == null) return;

            int imageWidth = options.ImageWidth;
            int imageHeight = options.ImageHeight;

            var builder = new ContainerBuilder();

            builder.RegisterType<TxtReader>().As<IReader>();

            
            if (options.IsText)
                builder.RegisterType<LiteratureExtractor>().As<IWordsExtractor>();
            else
                builder.RegisterType<LineByLineExtractor>().As<IWordsExtractor>();


            if (!options.PartsOfSpeechWhiteList.Any())
            {
                builder.RegisterInstance(new BoringWordsConfig(new List<Word.PartOfSpeech>
                {
                    Word.PartOfSpeech.Adjective,
                    Word.PartOfSpeech.Adverb,
                    Word.PartOfSpeech.Noun,
                    Word.PartOfSpeech.Verb,
                })).As<BoringWordsConfig>();
            }
            else
            {
                builder.RegisterInstance(
                        new BoringWordsConfig(
                            PartsOfSpeechListFromStrings(options.PartsOfSpeechWhiteList)
                            ))
                    .As<BoringWordsConfig>();
            }
            

            builder.RegisterType<ToLowercase>().As<IPreprocessor>();
            builder.RegisterType<ExcludeBoringWords>().As<IPreprocessor>();

            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();


            FontFamily fontFamily;
            try
            {
                fontFamily = new FontFamily(options.Font);
            }
            catch
            {
                fontFamily = FontFamily.GenericSansSerif;
            }
            builder.RegisterInstance(fontFamily).As<FontFamily>();

            switch (options.Color)
            {
                case "word":
                    builder.RegisterType<WordColorer>().As<IColorer>();
                    break;
                default:
                    var color = Color.FromName(options.Color);
                    builder.RegisterInstance(new ConstantColorer(color)).As<IColorer>();
                    break;
            }

            switch (options.OutputFormat)
            {
                case "jpeg":
                case "jpg":
                    builder.RegisterType<JpegWriter>().As<IImageWriter>();
                    break;
                case "bmp":
                    builder.RegisterType<BmpWriter>().As<IImageWriter>();
                    break;
                case "png":
                    builder.RegisterType<PngWriter>().As<IImageWriter>();
                    break;
                default:
                    Console.WriteLine($"Given wrong output format {options.OutputFormat}. Reverting to png.");
                    builder.RegisterType<PngWriter>().As<IImageWriter>();
                    break;
            }

            builder.RegisterType<TagsCloudGenerator>().AsSelf();
            
            var container = builder.Build();

            var tagsCloud = container.Resolve<TagsCloudGenerator>();

            tagsCloud.GenerateFromFile(options.InputFile, options.OutputFile, imageWidth, imageHeight);
        }

        private static List<Word.PartOfSpeech> PartsOfSpeechListFromStrings(IEnumerable<string> partsOfSpeechWhitelist)
        {
            var posWhitelist = new List<Word.PartOfSpeech>();
            foreach (var pos in partsOfSpeechWhitelist)
            {
                switch (pos)
                {
                    case "adjective": 
                        posWhitelist.Add(Word.PartOfSpeech.Adjective);
                        break;
                    case "adverb": 
                        posWhitelist.Add(Word.PartOfSpeech.Adverb); 
                        break;
                    case "adverb-pronoun": 
                        posWhitelist.Add(Word.PartOfSpeech.AdverbPronoun); 
                        break;
                    case "adjective-numeral": 
                        posWhitelist.Add(Word.PartOfSpeech.AdjectiveNumeral); 
                        break;
                    case "adjective-pronoun": 
                        posWhitelist.Add(Word.PartOfSpeech.AdjectivePronoun); 
                        break;
                    case "composite": 
                        posWhitelist.Add(Word.PartOfSpeech.Composite); 
                        break;
                    case "conjunction": 
                        posWhitelist.Add(Word.PartOfSpeech.Conjunction); 
                        break;
                    case "interjection": 
                        posWhitelist.Add(Word.PartOfSpeech.Interjection); 
                        break;
                    case "numeral": 
                        posWhitelist.Add(Word.PartOfSpeech.Numeral); 
                        break;
                    case "particle": 
                        posWhitelist.Add(Word.PartOfSpeech.Particle); 
                        break;
                    case "preposition": 
                        posWhitelist.Add(Word.PartOfSpeech.Preposition);
                        break;
                    case "noun": 
                        posWhitelist.Add(Word.PartOfSpeech.Noun); 
                        break;
                    case "noun-pronoun": 
                        posWhitelist.Add(Word.PartOfSpeech.NounPronoun);
                        break;
                    case "verb": 
                        posWhitelist.Add(Word.PartOfSpeech.Verb);
                        break;
                }
            }

            return posWhitelist;
        }
    }
}
