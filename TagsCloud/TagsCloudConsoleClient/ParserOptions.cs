using CommandLine;
using System.Collections.Generic;

namespace TagsCloudConsoleClient
{
    internal class ParserOptions
    {
        [Option('r', "ReadFrom", Required = true)]
        public string PathToRead { get; set; }

        [Option('s', "SaveTo", Required = true)]
        public string PathToSave { get; set; }

        [Option('p', "ParserId", Required = false)]
        public string ParserFactorialId { get; set; }

        [Option('c', "ConvertersIds", Required = false)]
        public IEnumerable<string> ConvertersFactorialIds { get; set; }

        [Option('f', "FiltersIds", Required = false)]
        public IEnumerable<string> FiltersFactorialIds { get; set; }

        [Option('d', "PainterId", Required = false)]
        public string PainterFactorialId { get; set; }

        [Option('w', "SaverId", Required = false)]
        public string SaverFactorialId { get; set; }

        [Option('u', "WordsLayouterId", Required = false)]
        public string WordsLayouterFactorialId { get; set; }

        [Option('l', "PointsSearcherId", Required = false)]
        public string PointsSearcherFactorialId { get; set; }

        [Option('g', "Colors", Required = false)]
        public IEnumerable<string> Colors { get; set; }

        [Option('b', "BackgroundColor", Required = false)]
        public string BackgroundColor { get; set; }

        [Option('m', "Sizes", Required = false)]
        public IEnumerable<int> WidthAndHeight { get; set; }

        [Option('t', "Font", Required = false)]
        public string FontName { get; set; }

        [Option('e', "TakenPartsOfSpeech", Required = false)]
        public IEnumerable<string> TakenPartsOfSpeech { get; set; }
    }
}