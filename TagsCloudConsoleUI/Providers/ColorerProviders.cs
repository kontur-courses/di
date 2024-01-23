using Autofac;
using TagsCloudContainer.Drawing.Colorers;

namespace TagsCloudConsoleUI.Providers;

public static class ColorerProviders
{
    public static readonly IReadOnlyDictionary<string, Action<ContainerBuilder>> RegisteredProviders = 
        new Dictionary<string, Action<ContainerBuilder>>
    {
        {"Random", builder => builder.RegisterType<RandomWordColorer>().As<IWordColorer>().SingleInstance()},
        {"Bicolor", builder => builder.RegisterInstance(new BicolorColorer()).As<IWordColorer>().SingleInstance()}
    };
}