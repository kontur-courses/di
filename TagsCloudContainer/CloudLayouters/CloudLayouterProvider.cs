using Autofac;

namespace TagsCloudContainer.CloudLayouters;

public class CloudLayouterProvider: ICloudLayouterProvider
{
    private readonly ILifetimeScope scope;

    public CloudLayouterProvider(ILifetimeScope scope)
    {
        this.scope = scope;
    }

    public ICloudLayouter Get()
    {
        return scope.Resolve<ICloudLayouter>();
    }
}