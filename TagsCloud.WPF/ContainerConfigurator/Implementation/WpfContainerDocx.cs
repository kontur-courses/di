using Autofac;
using TagsCloud.FileConverter;
using TagsCloud.FileConverter.Implementation;
using TagsCloud.FileReader;
using TagsCloud.FileReader.Implementation;
using TagsCloud.WordHandler;
using TagsCloud.WordHandler.Implementation;
using TagsCloud.WPF.PictureSaver;
using TagsCloud.WPF.PictureSaver.Implementation;

namespace TagsCloud.WPF.ContainerConfigurator.Implementation;

public class WpfContainerDocx : IContainerConfigurator
{
    private const string Path = "../../../Words.docx";

    public IContainer GetContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<MainWindow>().SingleInstance();
        builder.Register(_ => new string(Path)).As<string>();
        builder.RegisterType<LowerCaseHandler>().As<IWordHandler>();
        builder.RegisterType<BoringRusWordsHandler>().As<IWordHandler>();
        builder.RegisterType<RecurringWordsHandler>().As<IWordHandler>();
        builder.RegisterType<PictureSaverCanvas>().As<IPictureSaver>();
        builder.RegisterType<DocxReader>().As<IFileReader>();
        builder.RegisterType<ConvertToTxt>().As<IFileConverter>();

        return builder.Build();
    }
}