using System.Drawing;
using ConsoleClient;
using Ninject;
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

ConfigureContainer().Get<Client>().Execute();

StandardKernel ConfigureContainer()
{
    var container = new StandardKernel();
    container.Bind<IWordsLoader>().ToConstant(new FileWordsLoader(filename));
    container.Bind<IWordsProcessor>().To<FakeWordsProcessor>();
    container.Bind<ICloudDrawer>().ToConstant(new BaseCloudDrawer(new FontFamily("Arial"), 50, 10,
        new Size(800, 600), Color.Black)).InSingletonScope();
    container.Bind<ICloudLayouter>().ToConstant(new CircularCloudLayouter(new Point(400, 300))).InSingletonScope();
    container.Bind<ICloudCreator>().To<DrawingCloudCreator>();
    return container;
}