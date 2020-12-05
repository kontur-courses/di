using Autofac;

namespace TagsCloudVisualisation
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OwnedByLifetimeScope();
        }
    }
}