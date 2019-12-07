using System;
using System.Diagnostics;
using System.Linq;
using TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands;

namespace TagsCloudContainer.ApplicationRunning.ConsoleApp
{
    public class ConsoleAppRunner : IAppRunner
    {
        private CommandsExecutor executor;
        public ConsoleAppRunner(CommandsExecutor executor)
        {
            this.executor = executor;
        }
        
        public void Run()
        {
            Console.WriteLine("Welcome to cloud visualizer. Use 'help' to see commands list.");
            while (true)
            {
                var args = Console.ReadLine()?.Split().ToArray();
                executor.Execute(args);
            }
        }
    }
}