using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using TagsCloud.WordPrework;

namespace TagsCloud
{
    public class Options
    {
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

        [Option("fontcolor", Required = false, Default = "Blue", HelpText = "Defines color of the font.")]
        public String FontColor { get; set; }


        [Option("boring", Required = false, HelpText = "Defines boring parts of speech that will not be listed in result. " +
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
    }
}
