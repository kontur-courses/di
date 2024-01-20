using System.Collections.Immutable;
using Autofac;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainer.Providers;

public static class WordProviders
{
    public static readonly IReadOnlyDictionary<string, Action<ContainerBuilder, string>> RegisteredProviders =
        new Dictionary<string, Action<ContainerBuilder, string>>
    {
        {".txt", (builder, path) => builder.RegisterInstance(new TxtFileWordParser(path)).As<IWordProvider>()}
    };
}