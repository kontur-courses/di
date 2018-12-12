using Autofac;
using TagCloud.Core.Settings;

namespace TagCloud.GUI
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterSettings<TSettings>(this ContainerBuilder builder) where TSettings : ISettings
        {
            builder.RegisterType<TSettings>()
                .As<ISettings>()
                .AsSelf()
                .SingleInstance();
        }
    }
}