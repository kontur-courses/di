using Autofac;
using TagCloud.Core.FileReaders;

namespace TagCloud
{
    public class TagCloudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(TxtReader).Assembly)
                .AsImplementedInterfaces();
        }
    }
}