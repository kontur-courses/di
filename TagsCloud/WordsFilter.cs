using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordsFilter
	{
		private readonly Func<string, bool> _filter;
		private readonly IEnumerable<string> _sourceWords;

		public WordsFilter(ITextReader reader, Func<string, bool> filter)
		{
			_filter = filter;
			_sourceWords = reader.Read();
		}

		public IEnumerable<string> GetWords() => _sourceWords.Where(_filter).Select(w => w.ToLower());

		public static Func<string, bool> GetDefaultFilter() => word => word.Length >= 3;
	}
}