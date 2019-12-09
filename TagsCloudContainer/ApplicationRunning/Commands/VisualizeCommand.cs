using System;
using System.Drawing;
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
            if(args.Length < 8) throw new ArgumentException("Incorrect arguments count! Expected 8.");
            var maker = BitmapMakers.TryGetBitmapMaker(args[0]);
            if(maker is null) throw new ArgumentException($"Incorrect bitmap maker value {args[0]}");
            if(!int.TryParse(args[1], out var width)
            || width <= 1) throw new ArgumentException($"Incorrect width value {args[2]}");
            if(!int.TryParse(args[2], out var height)
            || height <= 1) throw new ArgumentException($"Incorrect height value {args[3]}");
            var backgroundColor = Color.FromName(args[3]);
            var firstColor = Color.FromName(args[4]);
            var secondColor = Color.FromName(args[5]);
            if(!bool.TryParse(args[6], out var isGradient)) throw new ArgumentException($"Incorrect is gradient value {args[6]}");
            var font = new Font(args[7], 16);
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