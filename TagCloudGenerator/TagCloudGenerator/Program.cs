using Autofac;
using TagCloudGenerator.Clients;
using TagCloudGenerator.Clients.CmdClient;
using TagCloudGenerator.GeneratorCore;
using TagCloudGenerator.GeneratorCore.CloudVocabularyPreprocessors;

namespace TagCloudGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var container = BuildContainer(args);
            container.Resolve<CloudGenerator>().CreateTagCloudImage();
        }

        private static IContainer BuildContainer(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(new CommandLineClient(args)).As<IClient>().SingleInstance();
            containerBuilder.RegisterType<CloudContextGenerator>().AsSelf().SingleInstance();
            containerBuilder
                .RegisterDecorator<CloudVocabularyPreprocessor, ExcludingPreprocessors, ToLowerPreprocessor>();
            containerBuilder.RegisterType<CloudGenerator>().AsSelf();

            return containerBuilder.Build();
        }
    }
}