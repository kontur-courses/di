using Autofac;
using Autofac.Builder;
using TagCloud.GUI.Settings;

namespace TagCloud.GUI.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TSettings, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterSettings<TSettings>(this ContainerBuilder builder) where TSettings : ISettings
        {
            return builder.RegisterType<TSettings>()
                .As<ISettings>()
                .AsSelf()
                .SingleInstance();
        }
    }
}