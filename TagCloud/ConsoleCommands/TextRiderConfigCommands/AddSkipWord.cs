using System.Collections.Generic;
using TextPreprocessor.TextRiders;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class AddSkipWord : IConsoleCommand
    {
        public string Name => "AddSkipWord";
        public string Description => "Добавляет слово в список для исключения";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            TextRiderConfig.SkipWords.Add(args["Word"].ToString());
        }

        public List<string> ArgsName => new List<string>() { "Word" } ;
    }
}