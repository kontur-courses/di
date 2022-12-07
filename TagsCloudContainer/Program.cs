using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.DI;
using TagsCloudContainer.Options;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var autofacServiceProviderFactory = new AutofacServiceProviderFactory(CloudContainerBuilder.Build);
            var builder = autofacServiceProviderFactory.CreateBuilder(new ServiceCollection());
            var container = builder.Build();
            try
            {
                while (true)
                {
                    var options = VisualizationOptionsHandler.RequestVisualizationOptions();
                    var cloudCreator = container.Resolve<TagCloudCreator>();
                    cloudCreator.CreateCloud(options);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}