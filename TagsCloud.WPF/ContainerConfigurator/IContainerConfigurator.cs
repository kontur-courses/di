using Autofac;

namespace TagsCloud.WPF.ContainerConfigurator;

public interface IContainerConfigurator
{
    public IContainer GetContainer();
}