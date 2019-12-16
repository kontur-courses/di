using System.Collections.Generic;
using System.Drawing;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class ImageSize : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public ImageSize(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "SetImageSize";
        public string Description => "Задает ширину и высоту картинки";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            painterConfig.ImageWidth = int.Parse(args["Width"].ToString());
            painterConfig.ImageHeight = int.Parse(args["Height"].ToString());
            painterConfig.CloudCenter = new Point(painterConfig.ImageWidth / 2, painterConfig.ImageHeight / 2);
        }
        public List<string> ArgsName => new List<string>() { "Width", "Height" };
    }
}