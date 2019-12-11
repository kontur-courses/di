using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordsProcessor: IWordsProcessor
	{
		private readonly ITextReader textReader;
		private readonly IEnumerable<IWordFilter> wordFilters;

		public WordsProcessor(ITextReader textReader, IEnumerable<IWordFilter> wordFilters)
		{
			this.textReader = textReader;
			this.wordFilters = wordFilters;
		}

		public IEnumerable<Word> GetWordsWithFrequencies()
		{
			var frequencies = new Dictionary<string, int>();
			foreach (var word in FilterWords())
			{
				if (frequencies.ContainsKey(word))
					frequencies[word]++;
				else
					frequencies[word] = 1;
			}

			return frequencies
				.OrderByDescending(pair => pair.Value)
				.Select(pair => new Word(pair.Key, pair.Value));
		}

		private IEnumerable<string> FilterWords() => 
			textReader.Read().Select(w => w.ToLower()).Where(word => wordFilters.All(f => f.CheckWord(word)));
	}
}