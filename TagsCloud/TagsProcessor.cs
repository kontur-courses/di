using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TagsProcessor
	{
		private readonly IEnumerable<Word> _words;
		private const int MaxFontSize = 30;

		public TagsProcessor(IWordsProcessor wordsProcessor) => 
			_words = wordsProcessor.GetSortedWordsWithFrequencies();

		public IEnumerable<Tag> GetTags()
		{
			// Not implemented
			return _words.Select(w => new Tag(w.Text, w.Frequency, new Rectangle()));
		}

		public static int CalculateFontSize(Word word)
		{
			throw new NotImplementedException();
		}
	}
}