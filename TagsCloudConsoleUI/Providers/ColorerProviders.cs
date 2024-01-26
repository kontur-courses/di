using Autofac;
using TagsCloudCore.Drawing.Colorers;

namespace TagsCloudConsoleUI.Providers;

public static class ColorerProviders
{
    public static readonly IReadOnlyDictionary<string, IWordColorer> RegisteredProviders = 
        new Dictionary<string, IWordColorer>
    {
        {"Random", new RandomWordColorer()},
        {"Bicolor", new BicolorColorer()}
    };
}