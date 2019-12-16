using System.Collections.Generic;
using TextPreprocessor.TextRiders;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class PathToFile : IConsoleCommand
    {
        private TextRiderConfig textRiderConfig;

        public PathToFile(TextRiderConfig textRiderConfig)
        {
            this.textRiderConfig = textRiderConfig;
        }
        
        public string Name => "PathToFile";
        public string Description => "Задает путь к файлу с тексом";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            textRiderConfig.FilePath = args["Path"].ToString();
        }

        public List<string> ArgsName => new List<string>() { "Path" };
    }
}