using System.Collections.Generic;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class SetImageName : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public SetImageName(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "SetImageName";
        public string Description => "Задает имя для изображения";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            var imageName = args["Name"].ToString();
            painterConfig.ImageName = imageName;
        }

        public List<string> ArgsName => new List<string>() {"Name"};
    }
}