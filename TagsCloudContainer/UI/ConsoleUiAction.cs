using System.IO;
using Autofac;

namespace TagsCloudContainer.UI
{
    public abstract class ConsoleUiAction : IUiAction
    {
        protected readonly ContainerBuilder builder;
        protected readonly TextReader reader;
        protected readonly TextWriter writer;
        public abstract string Category { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public ConsoleUiAction(TextReader reader, TextWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        public ConsoleUiAction
            (TextReader reader, TextWriter writer, ContainerBuilder builder)
            : this(reader, writer)
        {
            this.builder = builder;
        }
       

        public abstract void Perform();
    }
}