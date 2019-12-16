using System.Collections.Generic;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class MaxFontSize : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public MaxFontSize(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "MaxFontSize";
        public string Description => "Задает максимальный размер шрифта";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            painterConfig.MaxFontSize = int.Parse(args["FontSize"].ToString());
        }

        public List<string> ArgsName => new List<string>() { "FontSize" };
    }
}