using Autofac;

namespace TagCloud.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .SingleInstance()
                .OwnedByLifetimeScope();
        }
    }
}