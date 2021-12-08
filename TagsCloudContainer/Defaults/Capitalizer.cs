using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class Capitalizer : IWordNormalizer
{
    public string Normalize(string word)
    {
        return $"{char.ToUpper(word[0])}{word[1..]}";
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<Capitalizer>().AsSelf().As<IWordNormalizer>();
    }
}
