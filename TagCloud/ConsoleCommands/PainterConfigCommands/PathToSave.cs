using System.Collections.Generic;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class PathToSave : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public PathToSave(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "PathToSave";
        public string Description => "Задает путь для сохранения картинки";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            painterConfig.PathForSave = args["Path"].ToString();
        }

        public List<string> ArgsName => new List<string>() { "Path" };
    }
}