using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.MyStem;

namespace TagsCloudContainer.WordProcessing.Converting
{
    public class ToInitialFormWordConverter : IWordConverter
    {
        private readonly MyStemExecutor myStemExecutor;
        private readonly MyStemResultParser myStemResultParser;

        public ToInitialFormWordConverter(MyStemExecutor myStemExecutor, MyStemResultParser myStemResultParser)
        {
            this.myStemExecutor = myStemExecutor;
            this.myStemResultParser = myStemResultParser;
        }

        public IEnumerable<string> ConvertWords(IEnumerable<string> words)
        {
            var myStemResult = myStemExecutor.GetMyStemResultForWords(words, "-ni");
            return myStemResultParser.GetInitialFormsByResultOfNiCommand(myStemResult, words)
                .Select(p => p.Item2);
        }
    }
}