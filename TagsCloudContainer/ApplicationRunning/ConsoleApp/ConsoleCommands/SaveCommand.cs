using System;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.CloudVisualizers.ImageSaving;

namespace TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands
{
    public class SaveCommand : IConsoleCommand
    {
        private TagsCloud cloud;
        private SettingsManager manager;
        public SaveCommand(TagsCloud cloud, SettingsManager manager)
        {
            this.cloud = cloud;
            this.manager = manager;
        }
        public void Act(string[] args)
        {
            if(args.Length < 2) throw new ArgumentException("Incorrect arguments count! Expected 2.");
            if(!Directory.Exists(args[0])) throw new ArgumentException($"Incorrect directory '{args[0]}'");
            var format = SupportedImageFormats.TryGetSupportedImageFormats(args[2]);
            if(format is null) throw new ArgumentException($"Incorrect image format '{args[2]}'");
            var fullPath = Path.Combine(args[0], args[1]);
            manager.ConfigureImageSaverSettings(format, fullPath);
            cloud.SaveVisualized();
            Console.WriteLine($"Successfully saved image at {fullPath}");
        }

        public string Name => "save";
        public string Description => "saves visualized cloud to file";
        public string Arguments => "path name format";
    }
}