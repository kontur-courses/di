using Autofac;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudConsoleUI.Providers;

public static class WordProviders
{
    public static readonly IReadOnlyDictionary<string, Action<ContainerBuilder, string>> RegisteredProviders =
        new Dictionary<string, Action<ContainerBuilder, string>>
    {
        {".txt", (builder, path) => builder.RegisterInstance(new TxtFileWordParser(path)).As<IWordProvider>().SingleInstance()},
        {".docx", (builder, path) => builder.RegisterInstance(new DocxFileWordParser(path)).As<IWordProvider>().SingleInstance()},
        {".doc", (builder, path) => builder.RegisterInstance(new DocxFileWordParser(path)).As<IWordProvider>().SingleInstance()}
    };
}