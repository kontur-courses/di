using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.WordsFileReading
{
    public class ParserSelector
    {
        private readonly IDictionary<string, IParser> parserByMode;

        public ParserSelector(IEnumerable<IParser> parsers)
        {
            parserByMode = new Dictionary<string, IParser>();

            foreach (var parser in parsers)
                parserByMode[parser.GetModeName()] = parser;
        }

        public IParser SelectParser(string mode)
        {
            if (parserByMode.ContainsKey(mode))
                return parserByMode[mode];
            throw new ArgumentException("Parsing mode is not supported");
        }
    }
}
