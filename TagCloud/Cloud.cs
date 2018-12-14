using Autofac;

namespace TagCloud
{
    public static class Cloud
    {
        public static CloudMaker CreateMaker(ClouderSettings settings)
        {
            var container = new ContainerBuilder();
            container.Register(c=>settings.DrawingSettings).AsSelf();
            container.RegisterType(settings.TCounter).As<IWordsCounter>();
            container.RegisterType(settings.TLayouter).As<ICloudLayouter>();            
            container.RegisterType<CloudDrawer>().AsSelf();
            container.RegisterType<CloudMaker>().AsSelf();
            return (CloudMaker) container.Build().Resolve(typeof(CloudMaker));
        }

        public static CloudMaker DefaultMaker() =>
            CreateMaker(ClouderSettings.Default());
    }
}