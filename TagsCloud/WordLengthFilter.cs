using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class WordLengthFilter: IWordFilter
	{
		public bool CheckWord(string word) => word.Length >= 3;
	}
}