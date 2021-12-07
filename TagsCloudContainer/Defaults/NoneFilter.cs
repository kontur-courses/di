using Autofac;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class NoneFilter : IWordFilter
{
    public bool IsValid(string word)
    {
        return true;
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<NoneFilter>().AsSelf().As<IWordFilter>();
    }
}
