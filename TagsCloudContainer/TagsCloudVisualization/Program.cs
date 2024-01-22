using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.PointsProviders;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.WordsAnalyzers;
using TextReader = TagsCloudVisualization.TextReaders.TextReader;

namespace TagsCloudVisualization;

public static class Program
{
    public static void Main()
    {
        var container = new ContainerBuilder();
        container.RegisterType<TxtTextReader>().As<TextReader>().WithParameter("path",
            @"C:\Users\Pobeda\Desktop\Practiti\.ШПОРА\di\TagsCloudContainer\TagsCloudVisualization\src\textSample.txt");
        container.RegisterType<TagProvider>();
        container.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>();
        container.RegisterType<ArchimedeanSpiralPointsProvider>().WithParameter("center", new Point(1000, 1000));
        container.RegisterType<TagsCloudVisualizator>().OnActivated(x => x.Instance.DrawTagsCloud().SaveAs(@"C:\Users\Pobeda\Desktop\Practiti\.ШПОРА\di\TagsCloudContainer\layoutImages", "tags", ImageFormat.Png));

        container.Build().Resolve<TagsCloudVisualizator>();
    }
}