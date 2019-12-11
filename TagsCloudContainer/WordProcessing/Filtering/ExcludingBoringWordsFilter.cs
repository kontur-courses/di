using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.MyStem;

namespace TagsCloudContainer.WordProcessing.Filtering
{
    public class ExcludingBoringWordsFilter : IWordFilter
    {
        private readonly MyStemExecutor myStemExecutor;
        private readonly MyStemResultParser myStemResultParser;

        public ExcludingBoringWordsFilter(MyStemExecutor myStemExecutor, MyStemResultParser myStemResultParser)
        {
            this.myStemExecutor = myStemExecutor;
            this.myStemResultParser = myStemResultParser;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            var myStemResult = myStemExecutor.GetMyStemResultForWords(words, "-ni");

            return myStemResultParser.GetPartsOfSpeechByResultOfNiCommand(myStemResult, words)
                .Where(p => !IsPartOfSpeechBoring(p.Item2))
                .Select(p => p.Item1);
        }

        private bool IsPartOfSpeechBoring(string myStemPartOfSpeech)
        {
            return myStemPartOfSpeech == "PR" || myStemPartOfSpeech.EndsWith("PRO") || myStemPartOfSpeech == "CONJ" ||
                   myStemPartOfSpeech == "PART";
        }
    }
}