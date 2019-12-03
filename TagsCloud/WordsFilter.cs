using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordsFilter: IWordsFilter
	{
		private readonly ITextReader _reader;
		private readonly Func<string, bool> _filter;

		public WordsFilter(ITextReader reader, Func<string, bool> filter)
		{
			_reader = reader;
			_filter = filter;
		}

		public IEnumerable<string> GetWords() => _reader.Read().Where(_filter).Select(w => w.ToLower());
	}
}