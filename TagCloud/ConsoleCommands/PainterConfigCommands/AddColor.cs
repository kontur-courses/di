using System.Collections.Generic;
using System.Drawing;
using FakeItEasy;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class AddColor : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public AddColor(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "AddColor";
        public string Description => "Добавляет цвет в формате ARGB в палитру";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            var A = int.Parse(args["A"].ToString());
            var R = int.Parse(args["R"].ToString());
            var G = int.Parse(args["G"].ToString());
            var B = int.Parse(args["B"].ToString());
            painterConfig.Palette.Add(Color.FromArgb(A,R,G,B));
        }

        public List<string> ArgsName => new List<string>() {"A", "R", "G", "B"};
    }
}