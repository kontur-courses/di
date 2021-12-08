using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class LowerNormalizer : IWordNormalizer
{
    public string Normalize(string word)
    {
        return word.ToLower();
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<LowerNormalizer>().AsSelf().As<IWordNormalizer>();
    }
}
