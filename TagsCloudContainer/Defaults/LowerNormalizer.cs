using Autofac;
using TagsCloudContainer.Registrations;
using TagsCloudVisualization.Abstractions;

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
