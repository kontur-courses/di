using Microsoft.Extensions.DependencyInjection;

namespace TagsCloud.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class InjectionAttribute : Attribute
{
    public InjectionAttribute(ServiceLifetime lifeTime)
    {
        LifeTime = lifeTime;
    }

    public ServiceLifetime LifeTime { get; }
}