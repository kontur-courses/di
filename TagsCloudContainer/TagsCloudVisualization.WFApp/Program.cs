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

static class Program
{
    [STAThread]
    static void Main()
    {
        var container = new ContainerBuilder();

        container.RegisterType<PaletteSettingsAction>().As<IUiAction>();
        container.RegisterType<TagsCloudAction>().As<IUiAction>();
        container.RegisterType<SourceSettingsAction>().As<IUiAction>();
        container.RegisterType<ImageSettingsAction>().As<IUiAction>();
        container.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();
        container.RegisterType<ImageSettings>().SingleInstance();
        container.RegisterType<SourceSettings>().SingleInstance();

        container.RegisterType<TxtTextReader>().As<TextReader>();
        container.RegisterType<TagProvider>();
        container.RegisterType<Palette>().SingleInstance();
        container.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>();
        container.RegisterType<ArchimedeanSpiralPointsProvider>().WithParameter("center", new Point(500, 500));
        container.RegisterType<TagsCloudVisualizator>();

        container.RegisterType<MainForm>();

        ApplicationConfiguration.Initialize();
        Application.Run(container.Build().Resolve<MainForm>());
    }
}