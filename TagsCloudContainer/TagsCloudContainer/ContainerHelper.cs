using DeepMorphy;
using DryIoc;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public static class ContainerHelper
{
    public static Container RegisterDefaultSingletonContainer()
    {
        var container = new Container(x => x.WithDefaultReuse(Reuse.Singleton));
        container.Register<MorphAnalyzer>();
        container.Register<IFunnyWordsSelector, DeepMorphyFunnyWordsSelector>();
        container.Register<MultiDrawer>(Reuse.Transient);
        container.Register<IDrawerFactory, ClassicDrawerFactory>();
        container.Register<IDrawerFactory, RandomColoredDrawerFactory>();
        container.Register<ILayouterAlgorithmFactory, CircularCloudLayouterFactory>();
        container.RegisterDelegate(r => r.Resolve<ISettingsFactory>().Build(), Reuse.Transient);
        return container;
    }
}