using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.TextFilters;


namespace TagsCloud.TextProcessing
{
    public class TextOperator
    {
        private readonly ITextReader[] textReaders;
        private readonly ITextFilter[] textFilters;
        private readonly IWordConverter[] wordConverters;

        public TextOperator(IEnumerable<ITextReader> textReaders, IEnumerable<ITextFilter> textFilters,
            IEnumerable<IWordConverter> wordConverters)
        {
            this.textReaders = textReaders.ToArray();
            this.textFilters = textFilters.ToArray();
            this.wordConverters = wordConverters.ToArray();
        }

        public IEnumerable<WordInfo> ReadFromFile(string path)
        {
            var reader = GetReader(path);
            var words = reader.ReadWords(path);
            var filtredWords = words
                                .Select(ConvertWord)
                                .Where(word => textFilters.All(filter => filter.CanTake(word)));

            var wordCount = new Dictionary<string, int>();
            foreach (var word in filtredWords)
            {
                if (!wordCount.ContainsKey(word))
                    wordCount[word] = 0;
                wordCount[word] += 1;
            }

            return wordCount.Select(pair => new WordInfo(pair.Key, pair.Value));
        }

        private string ConvertWord(string word) => wordConverters
                                                    .Aggregate(word, (current, converter) => converter.Convert(current));
        private ITextReader GetReader(string path)
        {
            var reader = textReaders.FirstOrDefault(r => r.CanRead(path));
            if (reader == null)
                throw new InvalidOperationException("This file type is not supported");
            return reader;
        }
    }
}
