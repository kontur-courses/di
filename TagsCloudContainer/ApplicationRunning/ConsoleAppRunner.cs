using System;
using FluentAssertions;

namespace TagsCloudContainer.ApplicationRunning
{
    public class ConsoleAppRunner : IAppRunner
    {
        private TagsCloud cloud;
        private SettingsManager settingsManager;
        public ConsoleAppRunner(TagsCloud cloud, SettingsManager settingsManager)
        {
            this.cloud = cloud;
            this.settingsManager = settingsManager;
        }
        
        public void Run()
        {
            Console.WriteLine("Hello! Welcome to cloud maker!");
        }
    }
}