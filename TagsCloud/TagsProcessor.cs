using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TagsProcessor: ITagsProcessor
	{
		private readonly IWordsProcessor _wordsProcessor;
		private const int MaxFontSize = 30;

		public TagsProcessor(IWordsProcessor wordsProcessor) => _wordsProcessor = wordsProcessor;

		public IEnumerable<Tag> GetTags()
		{
			// Not implemented
			return _wordsProcessor.GetSortedWordsWithFrequencies()
				.Select(w => new Tag(w.Text, w.Frequency, new Rectangle()));
		}

		public static int CalculateFontSize(Word word)
		{
			throw new NotImplementedException();
		}
	}
}