using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.CommandsExecuting;

namespace TagsCloudContainer.MyStem
{
    public class MyStemExecutor
    {
        private readonly CmdCommandExecutor commandExecutor;
        private readonly string pathToMyStemDirectory;

        private const string NameOfTempFile = "temp.txt";
        private const string NameOfMyStemFile = "mystem.exe";

        public MyStemExecutor(CmdCommandExecutor commandExecutor, string pathToMyStemDirectory)
        {
            this.commandExecutor = commandExecutor;
            this.pathToMyStemDirectory = pathToMyStemDirectory;
        }

        public string GetMyStemResultForWords(IEnumerable<string> words, string command)
        {
            var pathToTempFile = Path.Combine(pathToMyStemDirectory, NameOfTempFile);
            File.WriteAllLines(pathToTempFile, words);
            var pathToMyStem = Path.Combine(pathToMyStemDirectory, NameOfMyStemFile);
            return commandExecutor.ExecuteCommand($"{pathToMyStem} {command} {pathToTempFile}");
        }
    }
}