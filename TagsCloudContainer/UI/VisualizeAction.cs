using System;
using System.IO;
using Autofac;

namespace TagsCloudContainer.UI
{
    public class VisualizeAction : IUiAction
    {
        private readonly TextWriter writer;
        private readonly TextReader reader;
        private readonly ContainerBuilder builder;
        public string Category => "Visualization";
        public string Name => "Visualize";
        public string Description { get; }

        public VisualizeAction(TextWriter writer, TextReader reader, ContainerBuilder builder)
        {
            this.writer = writer;
            this.reader = reader;
            this.builder = builder;
        }

        public void Perform()
        {
            writer.WriteLine("Visualize with current settings, yes or no? 'y', 'n'");
            while (true)
            {
                var answer = reader.ReadLine();
                switch (answer)
                {
                    case "y":
                        var container = builder.Build();
                        Program.Visualize(container);
                        Environment.Exit(0);
                        return;
                    case "n":
                        return;
                    default:
                        writer.WriteLine("Answer should be 'y' or 'n'");
                        break;
                }
            }
        }
    }
}