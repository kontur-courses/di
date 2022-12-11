using DeepMorphy;
using DeepMorphy.Model;
using System.Collections.Generic;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class MorphsParser : IMorphsParser
    {
        public IEnumerable<MorphInfo> GetMorphs(string filePath)
        {
            var morph = new MorphAnalyzer(true);

            var text = TextReader.ReadFile(filePath);

            var textSplitter = new TextSplitter();
            var words = textSplitter.SplitTextOnWords(text);

            var morphInfo = morph.Parse(words);

            return morphInfo;
        }
    }
}
