using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordsProcessor: IWordsProcessor
	{
		private readonly IEnumerable<string> _words;
		private readonly Dictionary<string, int> _frequencies = new Dictionary<string, int>();

		public WordsProcessor(WordsFilter wordsFilter) => _words = wordsFilter.GetWords();

		public IEnumerable<Word> GetSortedWordsWithFrequencies()
		{
			foreach (var word in _words)
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
	}
}