using System;
using System.Dynamic;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouters;

namespace TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands
{
    public class GenerateCloudCommand : IConsoleCommand
    {
        private TagsCloud cloud;
        private SettingsManager manager;
        public GenerateCloudCommand(TagsCloud cloud, SettingsManager manager)
        {
            this.cloud = cloud;
            this.manager = manager;
        }
        
        public void Act(string[] args)
        {
            if(args.Length < 4) throw new ArgumentException("Incorrect arguments count! Expected 4.");
            if(!double.TryParse(args[2], out var step)) throw new ArgumentException($"Incorrect step value {args[2]}");
            if(!int.TryParse(args[3], out var broadness)) throw new ArgumentException($"Incorrect broadness value {args[3]}");
            if (!int.TryParse(args[1], out var size)) throw new ArgumentException($"Incorrect size value {args[1]}");
            var algorithm = CloudLayoutingAlgorithms.TryGetLayoutingAlgorithm(args[0], step, broadness);
            if(algorithm == null) throw new ArgumentException($"Incorrect algorithm name {args[0]}");
            manager.ConfigureLayouterSettings(algorithm, size, step, broadness);
            cloud.GenerateTagCloud();
            Console.WriteLine("Successfully generated cloud.");
        }
        public string Name => "generate";
        public string Description => "generate tag cloud using settings";
        public string Arguments => "algorithm size step broadness";
    }
}