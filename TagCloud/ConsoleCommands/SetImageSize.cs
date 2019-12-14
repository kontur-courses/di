using System.Collections.Generic;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class SetImageSize : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public SetImageSize(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "SetImageSize";
        public string Description => "Задает ширину и высоту картинки";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            painterConfig.ImageWidth = int.Parse(args["Width"].ToString());
            painterConfig.ImageHeight = int.Parse(args["Height"].ToString());
        }
        public List<string> ArgsName => new List<string>() { "Width", "Height" };
    }
}