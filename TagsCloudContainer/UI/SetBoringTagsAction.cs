using System;
using System.IO;
using System.Linq;
using Autofac;

namespace TagsCloudContainer.UI
{
    public class SetBoringTagsAction : ConsoleUiAction
    {
        public override string Category => "Preprocessors";
        public override string Name => "SetBoringTags";
        public override string Description { get; }

        public SetBoringTagsAction(TextReader reader, TextWriter writer, ContainerBuilder builder)
            : base(reader, writer, builder)
        {
        }

        public override void Perform()
        {
            writer.WriteLine("Enter all words, you don`t want to see in cloud," +
                             "splitted by whiteSpace");
            writer.WriteLine("Dont forget to activate BoringTagsFilter!");
            var tags = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
            builder.RegisterInstance(tags).AsSelf();
        }
    }
}