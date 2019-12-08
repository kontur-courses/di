using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordsProcessor: IWordsProcessor
	{
		private readonly ITextReader _textReader;
		private readonly IEnumerable<IWordFilter> _wordFilters;
		private readonly Dictionary<string, int> _frequencies;

		public WordsProcessor(ITextReader textReader, IEnumerable<IWordFilter> wordFilters)
		{
			_textReader = textReader;
			_wordFilters = wordFilters;
			_frequencies = new Dictionary<string, int>();
		}

		public IEnumerable<Word> GetSortedWordsWithFrequencies()
		{
			foreach (var word in FilterWords().Select(w => w.ToLower()))
			{
				if (_frequencies.ContainsKey(word))
					_frequencies[word]++;
				else
					_frequencies[word] = 1;
			}

			return _frequencies
				.OrderByDescending(pair => pair.Value)
				.Select(pair => new Word(pair.Key, pair.Value));
		}

		private IEnumerable<string> FilterWords() => 
			_textReader.Read().Where(word => _wordFilters.All(f => f.CheckWord(word)));
	}
}