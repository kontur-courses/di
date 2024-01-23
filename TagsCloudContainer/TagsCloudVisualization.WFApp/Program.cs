using Autofac;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Common;
using TagsCloudVisualization.PointsProviders;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.WFApp.Actions;
using TagsCloudVisualization.WFApp.Common;
using TagsCloudVisualization.WFApp.Infrastructure;
using TagsCloudVisualization.WordsAnalyzers;
using TextReader = TagsCloudVisualization.TextReaders.TextReader;

namespace TagsCloudVisualization.WFApp;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var container = new ContainerBuilder();

        container.RegisterAssemblyTypes(typeof(IUiAction).Assembly).AsImplementedInterfaces();
        container.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();
        container.RegisterType<ImageSettings>().SingleInstance();
        container.RegisterType<SourceSettings>().SingleInstance();
        container.RegisterType<ArchimedeanSpiralSettings>().SingleInstance();

        container.RegisterType<TxtTextReader>().As<TextReader>();
        container.RegisterType<TagProvider>();
        container.RegisterType<TagsSettings>().SingleInstance();
        container.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>();
        container.RegisterType<ArchimedeanSpiralPointsProvider>().WithParameter("center", new Point(500, 500));
        container.RegisterType<TagsCloudVisualizator>();

        container.RegisterType<MainForm>();

        ApplicationConfiguration.Initialize();
        Application.Run(container.Build().Resolve<MainForm>());
    }
}