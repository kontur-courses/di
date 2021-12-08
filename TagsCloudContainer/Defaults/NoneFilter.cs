using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

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
