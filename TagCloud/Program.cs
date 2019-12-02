using System;
using System.Linq;
using Autofac;
using TagCloud.CloudLayouter;
using TagCloud.Visualization;

namespace TagCloud
{
    public static class Program
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagCloudVisualization>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().AsSelf().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf();
            builder.RegisterType<VisualizationSettings>().AsSelf();
            builder.RegisterType<TextPreparer>().AsSelf();
            builder.RegisterType<TextPreparerSettings>().AsSelf();
            var container = builder.Build();

            var textPreparer = container.Resolve<TextPreparer>();

            foreach (var word in textPreparer.GetParsedTextDictionary().OrderByDescending(c => c.Value))
                Console.WriteLine(word);

            var visualization = container.Resolve<TagCloudVisualization>();
            visualization.MakeTagCloud();
            visualization.SaveTagCloud();
        }
    }
}