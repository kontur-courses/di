using System;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.BitmapMakers;

namespace TagsCloudContainer.ApplicationRunning.Commands
{
    public class VisualizeCommand : IConsoleCommand
    {
        private TagsCloud cloud;
        private SettingsManager manager;
        public VisualizeCommand(TagsCloud cloud, SettingsManager manager)
        {
            this.cloud = cloud;
            this.manager = manager;
        }

        public void Act(string[] args)
        {
            Check.ArgumentsCountIs(args, 8);
            var maker = BitmapMakers.TryGetBitmapMaker(args[0]);
            Check.Argument(args[0], maker != null);
            Check.Argument(args[1], int.TryParse(args[1], out var width), width > 1);
            Check.Argument(args[2], int.TryParse(args[2], out var height), height > 1);
            var backgroundColor = Color.FromName(args[3]);
            var firstColor = Color.FromName(args[4]);
            var secondColor = Color.FromName(args[5]);
            Check.Argument(args[6], bool.TryParse(args[6], out var isGradient));
            var fontName = string.Join(" ", args.Skip(7)).Trim('\'');
            var font = new Font(fontName, 16);
            var palette = new Palette
            {
                BackgroundColor = backgroundColor, IsGradient = isGradient, PrimaryColor = firstColor,
                SecondaryColor = secondColor
            };
            Visualize(palette, maker, width, height, font);
            Console.WriteLine("Successfully visualized cloud. Ready to save.");
        }

        private void Visualize(Palette palette, IBitmapMaker maker, int width, int height, Font font)
        {
            manager.ConfigureVisualizerSettings(palette, maker, width, height, font);
            cloud.VisualizeCloud();
        }

        public string Name => "visualize";
        public string Description => "visualize generated cloud";
        public string Arguments => "'bitmap maker' width height 'background color' 'first color' 'second color' 'is gradient' 'font name'";
    }
}