using Autofac;

namespace TagsCloud.ContainerConfigurator;

public interface IContainerConfigurator
{
    public IContainer GetContainer();
}