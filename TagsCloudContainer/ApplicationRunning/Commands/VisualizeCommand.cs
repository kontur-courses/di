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
            if(args.Length < 6) throw new ArgumentException("Incorrect arguments count! Expected 6.");
            var maker = BitmapMakers.TryGetBitmapMaker(args[0]);
            if(!int.TryParse(args[1], out var width)) throw new ArgumentException($"Incorrect width value {args[2]}");
            if(!int.TryParse(args[2], out var height)) throw new ArgumentException($"Incorrect height value {args[3]}");
            var backgroundColor = Color.FromName(args[3]);
            var firstColor = Color.FromName(args[4]);
            var secondColor = Color.FromName(args[5]);
            var palette = new Palette
            {
                BackgroundColor = backgroundColor, IsGradient = false, PrimaryColor = firstColor,
                SecondaryColor = secondColor
            };
            Visualize(palette, maker, width, height);
            Console.WriteLine("Successfully visualized cloud. Ready to save.");
        }

        private void Visualize(Palette palette, IBitmapMaker maker, int width, int height)
        {
            manager.ConfigureVisualizerSettings(palette, maker, width, height);
            cloud.VisualizeCloud();
        }

        public string Name => "visualize";
        public string Description => "visualize generated cloud";
        public string Arguments => "'bitmap maker' width height 'background color' 'first color' 'second color'";
    }
}