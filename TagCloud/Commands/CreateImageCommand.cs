using System;
using System.Collections.Generic;
using System.Text;
using TagCloud.Renderers;
using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class CreateImageCommand : ICommand
    {
        public string CommandId { get; } = "create";
        public string Description { get; } = "Create image from tag cloud";

        private IRender render;
        private readonly ResultSettings settings;

        public CreateImageCommand(IRender render, ResultSettings settings)
        {
            this.render = render;
            this.settings = settings;
        }

        public void Handle(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify file name");
                return;
            }

            settings.Name = args[0];
            render.Render();
            Console.WriteLine($"Created in {settings.OutputPath}");
        }
    }
}
