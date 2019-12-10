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
            Check.ArgumentsCountIs(args, 4);
            Check.Argument(args[2], double.TryParse(args[2], out var step), step >= 0.1);
            Check.Argument(args[3], int.TryParse(args[3], out var broadness), broadness >= 1, broadness <= 2);
            Check.Argument(args[1], int.TryParse(args[1], out var size), size > 0);
            var algorithm = CloudLayoutingAlgorithms.TryGetLayoutingAlgorithm(args[0], step, broadness);
            Check.Argument(args[0], algorithm != null);
            Generate(step, broadness, size, algorithm);
            Console.WriteLine("Successfully generated cloud.");
        }

        private void Generate(double step, int broadness, int size, ICloudLayoutingAlgorithm algorithm)
        {
            manager.ConfigureLayouterSettings(algorithm, size, step, broadness);
            cloud.GenerateTagCloud();
        }

        public string Name => "generate";
        public string Description => "generate tag cloud using settings";
        public string Arguments => "algorithm size[1...] step[0.1...] broadness[1, 2]";
    }
}