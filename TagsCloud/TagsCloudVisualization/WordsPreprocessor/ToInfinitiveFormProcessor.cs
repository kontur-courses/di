using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.WordsPreprocessor
{
    public class ToInfinitiveFormProcessor : IWordsPreprocessor
    {
        private readonly WordList _wordList;

        public ToInfinitiveFormProcessor(Stream dictionaryStream, Stream affixStream)
        {
            _wordList = WordList.CreateFromStreams(dictionaryStream, affixStream);
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Select(word => _wordList.CheckDetails(word).Root ?? word);
        }
    }
}