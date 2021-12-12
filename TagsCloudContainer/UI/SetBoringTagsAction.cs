using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac;

namespace TagsCloudContainer.UI
{
    public class SetBoringTagsAction : IUiAction
    {
        private readonly TextWriter writer;
        private readonly TextReader reader;
        private readonly ContainerBuilder builder;
        public string Category => "Preprocessors";
        public string Name => "SetBoringTags";
        public string Description { get; }

        public SetBoringTagsAction(TextWriter writer, TextReader reader, ContainerBuilder builder)
        {
            this.writer = writer;
            this.reader = reader;
            this.builder = builder;
        }

        public void Perform()
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