using System.Linq;
using System.Reflection;
using Autofac;

namespace TagCloud
{
    public static class Cloud
    {
        public static CloudMaker CreateMaker(CloudSettings cloudSettings,DrawingSettings drawingSettings)
        {
            var container = new ContainerBuilder();
            container.RegisterTypeByPrefixAs<ICloudLayouter>(cloudSettings.TLayouter);
            container.RegisterTypeByPrefixAs<IWordsCounter>(cloudSettings.TCounter);
            container.RegisterTypeByPrefixAs<IWeightScaler>(cloudSettings.TScaler);
            container.Register(c=>cloudSettings).AsSelf();     
            container.Register(c=>drawingSettings).AsSelf();          
            container.RegisterType<CloudDrawer>().AsSelf();
            container.RegisterType<CloudMaker>().AsSelf();
            return (CloudMaker) container.Build().Resolve(typeof(CloudMaker));
        }

        private static void RegisterTypeByPrefixAs<T>(this ContainerBuilder container, string prefix)
        {
            var asm = Assembly.GetExecutingAssembly();
            container.RegisterAssemblyTypes(asm)
                .Where(x => x.GetInterfaces().Contains(typeof(T)) && x.Name.StartsWith(prefix))
                .As<T>();
        }

        public static CloudMaker DefaultMaker() =>
            CreateMaker(CloudSettings.Default(),DrawingSettings.Default());
    }
}