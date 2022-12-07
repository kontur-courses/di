using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MystemHandler;
using TagCloud2.Interfaces;

namespace TagCloud2
{
    public class DataParser : IDataParser
    {
        private double _maxValue;
        public double MaxValue => _maxValue;
        private double _minValue = 1;
        public double MinValue => _minValue;

        private Dictionary<string, int> _frequencyDict = new();
        private int _totalWords;
        public void ParseText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("text is null or empty");

            // TODO вынести эту зависимость наверх
            MystemMultiThread mystem = new(1, @"mystem.exe");

            var words = mystem.StemWords(text)!
                .Where(l => !l.IsSlug)
                .Select(l => l.Lemma.ToLower());
            //var separator = new char[]
            //{
            //    ' ', '\n', '.', ','
            //};
            //var words = text
            //    .Split(
            //        separator,
            //        StringSplitOptions.RemoveEmptyEntries)
            //    .Where(s => !string.IsNullOrWhiteSpace(s))
            //    .Select(w => w.ToLower());

            foreach (var word in words)
            {
                _totalWords++;

                if (_frequencyDict!.ContainsKey(word))
                    _frequencyDict[word]++;
                else
                    _frequencyDict.Add(word, 1);
            }
        }

        public void ParseFile(string filepath)
        {
            using StreamReader reader = File.OpenText(filepath);
            ParseText(reader.ReadToEnd());
        }

        public IEnumerable<ValueTuple<string, double>> GetWordsWithFrequency()
        {
            _frequencyDict = _frequencyDict
                .OrderByDescending(x => x.Value)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value
                );
            _maxValue = (double)_frequencyDict.Values.Max() / _totalWords;
            _minValue = (double)_frequencyDict.Values.Min() / _totalWords;
            return _frequencyDict.Select(pair => (pair.Key, (double)pair.Value / _totalWords));
        }
    }
}