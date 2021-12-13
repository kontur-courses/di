using System;
using System.IO;
using Autofac;

namespace TagsCloudContainer.UI
{
    public class VisualizeAction : ConsoleUiAction
    {
        public override string Category => "Visualization";
        public override string Name => "Visualize";
        public override string Description { get; }

        public VisualizeAction
            (TextReader reader, TextWriter writer, ContainerBuilder builder)
            :base(reader, writer, builder)
        {
        }

        public override void Perform()
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