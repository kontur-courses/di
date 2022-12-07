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
    builder.RegisterInstance(new FileWordsLoader(filename)).As<IWordsLoader>();
    builder.RegisterType<FakeWordsProcessor>().As<IWordsProcessor>();
    builder.RegisterInstance(new BaseCloudDrawer(new FontFamily("Arial"), 50, 10,
        new Size(800, 600), Color.Black)).As<ICloudDrawer>();
    builder.RegisterInstance(new CircularCloudLayouter(new Point(400, 300))).As<ICloudLayouter>();
    builder.RegisterType<DrawingCloudCreator>().As<ICloudCreator>();
    builder.RegisterType<Client>();
    return builder.Build();
}