using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.CommandsExecuting;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.MyStem;

namespace TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying
{
    public class MyStemPartOfSpeechQualifier : IPartOfSpeechQualifier
    {
        private readonly CmdCommandExecutor commandExecutor;
        private readonly MyStemResultParser myStemResultParser;
        private readonly string pathToMyStemDirectory;

        private const string NameOfTempFile = "temp.txt";
        private const string NameOfMyStemFile = "mystem.exe";

        public MyStemPartOfSpeechQualifier(CmdCommandExecutor commandExecutor, MyStemResultParser myStemResultParser,
            string pathToMyStemDirectory)
        {
            this.commandExecutor = commandExecutor;
            this.myStemResultParser = myStemResultParser;
            this.pathToMyStemDirectory = pathToMyStemDirectory;
        }

        public IEnumerable<(string, PartOfSpeech)> QualifyPartsOfSpeech(IEnumerable<string> words)
        {
            var pathToTempFile = Path.Combine(pathToMyStemDirectory, NameOfTempFile);
            File.WriteAllLines(pathToTempFile, words);
            var pathToMyStem = Path.Combine(pathToMyStemDirectory, NameOfMyStemFile);
            var myStemResult = commandExecutor.ExecuteCommand($"{pathToMyStem} -ni {pathToTempFile}");
            return myStemResultParser.GetPartsOfSpeechByResultOfNiCommand(myStemResult, words);
        }
    }
}