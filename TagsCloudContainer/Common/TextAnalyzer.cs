using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NHunspell;
using TagsCloudContainer.Common.Contracts;

namespace TagsCloudContainer.Common
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly IFileReader[] fileReaders;
        private readonly Hunspell hunspellDict;
        private readonly IWordFilter[] wordFilters;

        public TextAnalyzer(IFileReader[] fileReaders, Hunspell hunspellDict, IWordFilter[] wordFilters = null)
        {
            this.fileReaders = fileReaders;
            this.hunspellDict = hunspellDict;
            this.wordFilters = wordFilters ?? new IWordFilter[0];
        }

        public Dictionary<string, int> GetWordStatisticsFromFile(string path)
        {
            return GetWordStatistics(GetWordsFromFile(path));
        }

        public Dictionary<string, int> GetWordStatistics(string text)
        {
            return GetWordStatistics(ParseWords(text));
        }

        private Dictionary<string, int> GetWordStatistics(IEnumerable<string> words)
        {
            var stat = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!TryStem(word, out var stem) ||
                    wordFilters.Any(filter => !filter.IsValid(stem)))
                    continue;
                
                if (!stat.ContainsKey(stem))
                    stat[stem] = 1;
                else
                    stat[stem]++;
            }
            
            return stat;
        }

        private IEnumerable<string> GetWordsFromFile(string path)
        {
            var format = Path.GetExtension(path).TrimStart('.');
            var fileReader = fileReaders.FirstOrDefault(reader =>
                reader.SupportedFormats.Contains(format, StringComparer.OrdinalIgnoreCase));

            if (fileReader == null)
                throw new ArgumentException($"Обработка файлов формата '{format}' не поддерживается");

            foreach (var line in fileReader.ReadLines(path))
            foreach (var word in ParseWords(line))
                yield return word;
        }

        private static IEnumerable<string> ParseWords(string text)
        {
            return text.Split()
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Select(TrimPunctuation);
        }

        private static string TrimPunctuation(string word)
        {
            var sb = new StringBuilder();
            foreach (var ch in word.Where(char.IsLetter))
                sb.Append(ch);

            return sb.ToString();
        }

        private bool TryStem(string word, out string stem)
        {
            stem = hunspellDict.Stem(word.ToLower()).FirstOrDefault();
            return stem != null;
        }
    }
}