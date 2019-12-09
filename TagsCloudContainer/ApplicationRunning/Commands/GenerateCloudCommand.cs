using System;
using TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands;
using TagsCloudContainer.CloudLayouters;

namespace TagsCloudContainer.ApplicationRunning.Commands
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
            if(!double.TryParse(args[2], out var step) 
               || step <= 0.1) throw new ArgumentException($"Incorrect step value {args[2]}");
            if(!int.TryParse(args[3], out var broadness) 
               || broadness <= 0
               || broadness > 2) throw new ArgumentException($"Incorrect broadness value {args[3]}");
            if (!int.TryParse(args[1], out var size) 
                || size <= 0) throw new ArgumentException($"Incorrect size value {args[1]}");
            var algorithm = CloudLayoutingAlgorithms.TryGetLayoutingAlgorithm(args[0], step, broadness);
            if(algorithm == null) throw new ArgumentException($"Incorrect algorithm name {args[0]}");
            Generate(step, broadness, size, algorithm);
            Console.WriteLine("Successfully generated cloud.");
        }

        public void Generate(double step, int broadness, int size, ICloudLayoutingAlgorithm algorithm)
        {
            manager.ConfigureLayouterSettings(algorithm, size, step, broadness);
            cloud.GenerateTagCloud();
        }

        public string Name => "generate";
        public string Description => "generate tag cloud using settings";
        public string Arguments => "algorithm size[1...] step[0.1...] broadness[1, 2]";
    }
}