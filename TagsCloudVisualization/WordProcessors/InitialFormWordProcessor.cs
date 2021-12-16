using System;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.WordProcessors
{
    public class InitialFormWordProcessor : ILiteraryWordProcessor
    {
        private readonly WordList wordList;
        
        public InitialFormWordProcessor(WordList? wordList)
        {
            this.wordList = wordList ?? throw new ArgumentNullException(nameof(wordList));
        }
        
        public string ProcessWord(string word)
        {
            return wordList.CheckDetails(word).Root ?? word;
        }
    }
}