using System.Reflection;
using Autofac;
using DeepMorphy;
using TagsCloudContainer.Visualizer.VisualizerSettings;
using Module = Autofac.Module;

namespace TagsCloudContainer
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => c.Resolve<IFactory<IVisualizerSettings>>().Create())
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterInstance(new MorphAnalyzer(withLemmatization:true)).SingleInstance();
        }
    }
}