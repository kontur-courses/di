using DeepMorphy;
using DeepMorphy.Model;
using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class MorphsParser : IMorphsParser
    {
        private ITextSplitter TextSplitter { get; }

        public MorphsParser(ITextSplitter textSplitter)
        {
            TextSplitter = textSplitter;
        }

        public IEnumerable<MorphInfo> GetMorphs(string filePath)
        {
            var morph = new MorphAnalyzer(true);

            var text = TextReader.ReadFile(filePath);

            var words = TextSplitter.SplitTextOnWords(text);

            var morphInfo = morph.Parse(words);

            return morphInfo;
        }
    }
}
