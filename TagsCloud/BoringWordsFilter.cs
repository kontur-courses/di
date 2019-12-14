using System.Collections.Generic;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class BoringWordsFilter: IWordFilter
	{
		private readonly ITextReader textReader;
		private HashSet<string> boringWords;
		
		public BoringWordsFilter(ITextReader textReader) => this.textReader = textReader;

		public bool CheckWord(string word)
		{
			PrepareWords();
			return !boringWords.Contains(word);
		}

		private void PrepareWords()
		{
			if (boringWords != null) return;
			boringWords = new HashSet<string>(textReader.Read());
		}
	}
}