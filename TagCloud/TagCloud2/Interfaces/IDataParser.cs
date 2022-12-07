using System;
using System.Collections.Generic;

namespace TagCloud2.Interfaces
{
    public interface IDataParser
    {
        public void ParseText(string text);
        public void ParseFile(string filepath);
        public IEnumerable<ValueTuple<string, double>> GetWordsWithFrequency();
        public double MinValue { get; }
        public double MaxValue { get; }
    }
}