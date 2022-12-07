using System.Drawing;
using Autofac;
using TagsCloudLayouter;

namespace TagCloud;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(
                new FileLoader(
                    @"Мы_Евгений_Замятин.txt"))
            .As<ITextLoader>();
        builder.RegisterInstance(new Palette(Color.Tan, Color.Teal)).AsSelf();
        builder.RegisterInstance(new SizeProperties(new Size(1024, 1024))).AsSelf();
        builder.RegisterInstance(new FontProperties()).AsSelf();
        builder.RegisterType<TextToTagsConverter>().As<ITextToTagsConverter>();
        builder.RegisterInstance(new CircularCloudLayouter(new Point(512, 512), 0.1, 0.1)).As<ICloudLayouter>();
        builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
        var container = builder.Build();

        var drawer = container.Resolve<ICloudDrawer>();
        var path = @"Cloud.jpg";
        drawer.Draw().Save(path);

        Console.WriteLine($"Tag cloud visualization saved to file {path}");
    }
}