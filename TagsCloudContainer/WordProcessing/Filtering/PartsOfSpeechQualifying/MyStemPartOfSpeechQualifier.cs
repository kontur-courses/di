using System;
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

        private static readonly string PathToMyStemDirectory =
            Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName, "WordProcessing",
                "Filtering", "PartsOfSpeechQualifying",
                "MyStem");
        private const string NameOfTempFile = "temp.txt";
        private const string NameOfMyStemFile = "mystem.exe";

        public MyStemPartOfSpeechQualifier(CmdCommandExecutor commandExecutor, MyStemResultParser myStemResultParser)
        {
            this.commandExecutor = commandExecutor;
            this.myStemResultParser = myStemResultParser;
        }

        public IEnumerable<(string, PartOfSpeech)> QualifyPartsOfSpeech(IEnumerable<string> words)
        {
            var pathToTempFile = Path.Combine(PathToMyStemDirectory, NameOfTempFile);
            File.WriteAllLines(pathToTempFile, words);
            var pathToMyStem = Path.Combine(PathToMyStemDirectory, NameOfMyStemFile);
            var myStemResult = commandExecutor.ExecuteCommand($"{pathToMyStem} -ni {pathToTempFile}");
            return myStemResultParser.GetPartsOfSpeechByResultOfNiCommand(myStemResult, words);
        }
    }
}