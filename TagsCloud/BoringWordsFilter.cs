using System.Collections.Generic;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class BoringWordsFilter: IWordFilter
	{
		private readonly HashSet<string> boringWords;
		
		public BoringWordsFilter(ITextReader textReader) => boringWords = new HashSet<string>(textReader.Read());

		public bool CheckWord(string word) => !boringWords.Contains(word);
	}
}