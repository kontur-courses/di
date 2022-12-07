using System.Drawing;
using System.Drawing.Imaging;
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
                    // var options = VisualizationOptionsHandler.RequestVisualizationOptions();
                    var options = new VisualizationOptions("D:/test.txt", ImageFormat.Bmp, new Size(1000, 1000), 2, 15);
                    var cloudCreator = container.Resolve<TagCloudCreator>();
                    cloudCreator.CreateCloud(options);
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}