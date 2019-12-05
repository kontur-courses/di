using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.WordProcessing.Filtering.CommandsExecuting;
using TagsCloudContainer.WordProcessing.Filtering.MyStem;

namespace TagsCloudContainer.WordProcessing.Filtering
{
    public class ExcludingBoringWordsFilter : IWordFilter
    {
        private readonly CmdCommandExecutor commandExecutor;
        private readonly MyStemResultParser myStemResultParser;
        private readonly string pathToMyStemDirectory;

        private const string NameOfTempFile = "temp.txt";
        private const string NameOfMyStemFile = "mystem.exe";

        public ExcludingBoringWordsFilter(CmdCommandExecutor commandExecutor, MyStemResultParser myStemResultParser,
            string pathToMyStemDirectory)
        {
            this.commandExecutor = commandExecutor;
            this.myStemResultParser = myStemResultParser;
            this.pathToMyStemDirectory = pathToMyStemDirectory;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            var pathToTempFile = Path.Combine(pathToMyStemDirectory, NameOfTempFile);
            File.WriteAllLines(pathToTempFile, words);
            var pathToMyStem = Path.Combine(pathToMyStemDirectory, NameOfMyStemFile);
            var myStemResult = commandExecutor.ExecuteCommand($"{pathToMyStem} -ni {pathToTempFile}");

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