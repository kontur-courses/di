using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordFiltersApplyer: IWordFiltersApplyer
	{
		private readonly ITextReader _reader;
		private readonly IEnumerable<Func<string, bool>> _filters;

		public WordFiltersApplyer(ITextReader reader, IEnumerable<Func<string, bool>> filters)
		{
			_reader = reader;
			_filters = filters;
		}

		public IEnumerable<string> GetWords() =>
			_reader.Read()
				.Where(word => _filters.All(filter => filter(word)))
				.Select(w => w.ToLower());
	}
}