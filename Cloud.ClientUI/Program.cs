using Cloud.ClientUI.ArgumentConverters;
using CloudContainer;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class Program
    {
        private readonly IArgumentConverter argumentConverter;
        private readonly IArgumentParser argumentParser;
        private readonly TagCloudContainer container;
        private readonly ISaver saver;

        public Program()
        {
        }

        public Program(TagCloudContainer container, IArgumentConverter argumentConverter,
            IArgumentParser argumentParser, ISaver saver)
        {
            this.container = container;
            this.argumentConverter = argumentConverter;
            this.argumentParser = argumentParser;
            this.saver = saver;
        }

        private static void Main(string[] args)
        {
            var container = new ServiceCollection();
            container.AddSingleton<TagCloudContainer, TagCloudContainer>();
            container.AddSingleton<ISaver, PngSaver>();
            container.AddSingleton<IArgumentConverter, ArgumentConverter>();
            container.AddSingleton<IArgumentParser, ArgumentParser>();
            container.AddSingleton<Program, Program>();
            container.AddSingleton<IArguments, ConvertedArguments>();
            container.BuildServiceProvider().GetService<Program>().Run(args);
        }

        private void Run(string[] args)
        {
            var parsedArguments = argumentParser.Parse(args);
            var convertedArguments = argumentConverter.ParseArguments(parsedArguments);
            saver.SaveImage(container.CreateTagCloud(convertedArguments), convertedArguments.OutputFileName);
        }
    }
}