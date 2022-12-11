using Autofac;
using TagsCloud.FileConverter;
using TagsCloud.FileConverter.Implementation;
using TagsCloud.FileReader;
using TagsCloud.FileReader.Implementation;
using TagsCloud.WordHandler;
using TagsCloud.WordHandler.Implementation;

namespace TagsCloud.ContainerConfigurator.Implementation;

public class WpfContainerTxt<T> : IContainerConfigurator
{
    private const string Path = "../../../Words.txt";

    public IContainer GetContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<T>().SingleInstance();
        builder.Register(_ => new string(Path)).As<string>();
        builder.RegisterType<LowerCaseHandler>().As<IWordHandler>();
        builder.RegisterType<BoringRusWordsHandler>().As<IWordHandler>();
        builder.RegisterType<RecurringWordsHandler>().As<IWordHandler>();
        builder.RegisterType<DocxReader>().As<IFileReader>();
        builder.RegisterType<ConvertToTxt>().As<IFileConverter>();

        return builder.Build();
    }
}