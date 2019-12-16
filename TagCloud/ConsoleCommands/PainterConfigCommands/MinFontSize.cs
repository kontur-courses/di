using System.Collections.Generic;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class MinFontSize : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public MinFontSize(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "MinFontSize";
        public string Description => "Задает минимальный размер шрифта";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            painterConfig.MaxFontSize = int.Parse(args["FontSize"].ToString());
        }

        public List<string> ArgsName => new List<string>() { "FontSize" };
    }
}