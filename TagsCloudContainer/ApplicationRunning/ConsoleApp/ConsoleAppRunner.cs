using System;
using System.Diagnostics;
using System.Linq;
using TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands;

namespace TagsCloudContainer.ApplicationRunning.ConsoleApp
{
    public class ConsoleAppRunner : IAppRunner
    {
        private TagsCloud cloud;
        private SettingsManager settingsManager;
        private CommandsExecutor executor;
        public ConsoleAppRunner(
            TagsCloud cloud, 
            SettingsManager settingsManager,
            CommandsExecutor executor)
        {
            this.cloud = cloud;
            this.settingsManager = settingsManager;
            this.executor = executor;
        }
        
        public void Run()
        {
            while (true)
            {
                var args = Console.ReadLine()?.Split().ToArray();
                executor.Execute(args);
            }
        }
    }
}