using System.Drawing;
using Autofac;
using CommandLine;
using ConsoleClient;
using TagCloud;
using TagCloud.Abstractions;

Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
{
    if (o.Source is null)
    {
        Console.Write("Source filepath: ");
        o.Source = Console.ReadLine();
    }

    if (o.Result is null)
    {
        Console.Write("Result filepath: ");
        o.Result = Console.ReadLine();
    }

    ConfigureContainer(o.Source).Resolve<Client>().Execute(o.Result);
});


IContainer ConfigureContainer(string sourceFilepath)
{
    var builder = new ContainerBuilder();

    builder.RegisterInstance(new FileWordsLoader(sourceFilepath))
        .As<IWordsLoader>().SingleInstance();

    var trimToLowerProcessor = new FuncWordsProcessor(words => words.Select(w => w.Trim().ToLower()));
    builder.RegisterInstance(trimToLowerProcessor)
        .As<IWordsProcessor>().SingleInstance();
    var bored = new[] { "мест", "предл", "союз", "част", "межд", "неизв" };
    builder.RegisterInstance(new MorphWordsProcessor(bored))
        .As<IWordsProcessor>().SingleInstance();

    builder.RegisterType<CountWordsTagger>().As<IWordsTagger>();

    var drawer = new BaseCloudDrawer(new FontFamily("Arial"), 50, 10, new Size(800, 600), Color.Black);
    builder.RegisterInstance(drawer)
        .As<ICloudDrawer>();
    var pointGenerator = new SpiralPointGenerator(0.1, 0.1);
    builder.RegisterInstance(new BaseCloudLayouter(new Point(400, 300), pointGenerator))
        .As<ICloudLayouter>();
    builder.RegisterType<DrawingCloudCreator>()
        .As<ICloudCreator>();

    builder.RegisterType<Client>().AsSelf().SingleInstance();

    return builder.Build();
}