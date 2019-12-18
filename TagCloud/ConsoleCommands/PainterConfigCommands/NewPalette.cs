using System.Collections.Generic;
using System.Drawing;
using TagCloudPainter;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class NewPalette : IConsoleCommand
    {
        private PainterConfig painterConfig;
        
        public NewPalette(PainterConfig config)
        {
            painterConfig = config;
        }
        
        public string Name => "NewPalette";
        public string Description => "Создает пустую паллитру";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            painterConfig.Palette = new List<Color>();
        }

        public List<string> ArgsName => new List<string>();
    }
}