using System;
using System.Collections.Generic;

using CommandLine;
using TagsCloud.TagsCloudVisualization;
using TagsCloud.TagsCloudVisualization.ColorSchemes;
using TagsCloud.WordPrework;

namespace TagsCloud
{
    public class Options
    {
        public static Options Parse(string[] args)
        {
            Options result = new Options();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => result = o)
                .WithNotParsed(e => throw new ArgumentException("Wrong command line arguments"));
            return result;
        }

        [Option('f', "file", Required = true, HelpText = "File with words to build tag cloud from.")]
        public string File{ get; set; }

        [Option('w', "width", Required = false, Default = 1024, HelpText = "Defines width of the picture.")]
        public int Width { get; set; }

        [Option('h', "height", Required = false, Default = 1024, HelpText = "Defines width of the picture.")]
        public int Height { get; set; }

        [Option("font", Required = false, Default = "Arial", HelpText = "Defines font of words.")]
        public string Font { get; set; }

        [Option("bgcolor", Required = false, Default = "LightGray", HelpText = "Defines color of the background.")]
        public String BackgroundColor { get; set; }

        [Option("colorScheme", Required = false, Default = ColorScheme.Red, HelpText = "Defines color scheme of the font.")]
        public ColorScheme ColorScheme { get; set; }


        [Option("boring", Required = false, Default = null, HelpText = "Defines boring parts of speech that will not be listed in result. " +
                                                           "Possible parts are: Adjective, Adverb, PronominalAdverb, NumeralAdjective, " +
                                                           "PronounAdjective, CompositePart, Conjunction, Interjection, Numeral, Particle, " +
                                                           "Pretext, Noun, PronounNoun, Verb")]
        public IEnumerable<PartOfSpeech> BoringParts { get; set; }


        [Option("only", Required = false, Default = null, HelpText = "Defines boring parts of speech that will not be listed in result. " +
                                                         "Possible parts are: Adjective, Adverb, PronominalAdverb, NumeralAdjective, " +
                                                         "PronounAdjective, CompositePart, Conjunction, Interjection, Numeral, Particle, " +
                                                         "Pretext, Noun, PronounNoun, Verb")]
        public IEnumerable<PartOfSpeech> PartsToUse { get; set; }

        [Option("infinitive", Required = false, Default = false, HelpText = "Uses infinitive form of words.")]
        public bool Infinitive { get; set; }

        [Option("dangle", Required = false, Default = Math.PI/16, HelpText = "Defines dangle in spiral cloud.")]
        public double DAngle { get; set; }

        [Option("minFontSize", Required = false, Default = 10, HelpText = "Defines minimum font size.")]
        public int MinFontSize { get; set; }

        [Option("maxFontSize", Required = false, Default = 100, HelpText = "Defines maximum font size.")]
        public int MaxFontSize { get; set; }

        [Option("sizeDefiner", Required = false, Default = SizeDefiner.Frequency, HelpText = "Defines type of the size definer." +
                                                                                             "Possible types are: Random, Frequency")]
        public SizeDefiner SizeDefinerType { get; set; }
    }
}
