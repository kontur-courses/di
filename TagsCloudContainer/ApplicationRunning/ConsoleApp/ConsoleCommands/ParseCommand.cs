using System;

namespace TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands
{
    public class ParseCommand : IConsoleCommand
    {
        public void Act(string[] args)
        {
            Console.WriteLine("Im parsing");
        }

        public string Name => "Parse";
        public string Description => "Parse words from path";
        public string Arguments => "path";
    }
}