using System.Drawing;
using Autofac;
using TagsCloudContainer.App;
using TagsCloudContainer.CloudItem;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.CounterNamespace;
using TagsCloudContainer.Distribution;
using TagsCloudContainer.MorphologicalAnalysis;
using TagsCloudContainer.Visualizer;

namespace TagsCloudContainer.Extensions
{
    public static class ContainerBuilderExtension
    {
        public static IContainer ConfigureTagCloud(this ContainerBuilder builder, Options options)
        {
            builder.Register(c => new ArchimedeanSpiral(new Point(options.CenterX, options.CenterY),
                    options.StepX, options.StepY, options.StepAngle))
                .As<IDistribution>().SingleInstance();
            builder.Register(c => new MorphologicalAnalyzer(options.InputFile))
                .AsSelf().SingleInstance();
            builder.RegisterType<TagCloudItem>().As<ICloudItem>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<Counter<Word>>().As<ICounter<Word>>();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>().SingleInstance();
            builder.RegisterType<TagCloudApp>().As<IApp>().SingleInstance();
            builder.RegisterInstance(options).AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}