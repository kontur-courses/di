using Autofac;
using TagsCloudGenerator.Client;

namespace TagsCloudGenerator
{
    class Program
    {
        private static void Main(string[] args)
        {
            using (var container = DependenciesBuilder.BuildContainer())
            {
                var client = container.Resolve<IClient>();
                var generator = container.Resolve<ICloudGenerator>();

                client.Run(generator);
            }
        }
    }
}