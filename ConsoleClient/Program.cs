using System.Drawing;
using Autofac;
using ConsoleClient;
using TagCloud;
using TagCloud.Abstractions;

Console.Write("Filename: ");
var filename = Console.ReadLine();
if (filename is null) throw new ArgumentException("Filename can't be null");
if (!File.Exists(filename))
{
    Console.WriteLine("File don't exist");
    return;
}

ConfigureContainer().Resolve<Client>().Execute();

IContainer ConfigureContainer()
{
    var builder = new ContainerBuilder();

    builder.RegisterInstance(new FileWordsLoader(filename))
        .As<IWordsLoader>().SingleInstance();

    var trimToLowerProcessor = new FuncWordsProcessor(words => words.Select(w => w.Trim().ToLower()));
    builder.RegisterInstance(trimToLowerProcessor)
        .As<IWordsProcessor>().SingleInstance();
    var bored = new[] { "мест", "предл", "союз", "част", "межд", "неизв" };
    builder.RegisterInstance(new MorphWordsProcessor(bored))
        .As<IWordsProcessor>().SingleInstance();

    var drawer = new BaseCloudDrawer(new FontFamily("Arial"), 50, 10, new Size(800, 600), Color.Black);
    builder.RegisterInstance(drawer)
        .As<ICloudDrawer>();
    builder.RegisterInstance(new CircularCloudLayouter(new Point(400, 300)))
        .As<ICloudLayouter>();
    builder.RegisterType<DrawingCloudCreator>()
        .As<ICloudCreator>();

    builder.RegisterType<Client>().AsSelf().SingleInstance();

    return builder.Build();
}