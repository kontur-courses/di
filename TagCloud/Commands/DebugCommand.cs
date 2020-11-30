using System;
using System.Collections.Generic;
using System.Text;
using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class DebugCommand : ICommand
    {
        public string CommandId { get; } = "debug";
        public string Description { get; } = "Just for debug";

        private readonly SourceSettings settings;

        public DebugCommand(SourceSettings sourceSettings)
        {
            settings = sourceSettings;
        }

        public void Handle(string[] args)
        {
            Console.WriteLine(settings.Destination);
        }
    }
}
