using System.Collections.Generic;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class BoringWordsFilter: IWordFilter
	{
		private HashSet<string> boringWords;
		private const string wordsSource = "../../Resources/boringWords.txt";
		
		public BoringWordsFilter() => boringWords = new HashSet<string>(File.ReadAllLines(wordsSource));

		public bool CheckWord(string word) => !boringWords.Contains(word);
	}
}