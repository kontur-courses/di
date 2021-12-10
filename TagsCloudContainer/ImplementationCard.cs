using TagsCloudContainer.Registrations;

namespace TagsCloudContainer;

public class ImplementationCard
{
    private readonly HashSet<Type> implementedServices = new();
    public Type Implementation { get; private set; }
    public IReadOnlySet<Type> ImplementedServices => implementedServices;
    public bool IsSingleton { get; private set; }

    public ImplementationCard(Type implementation, IEnumerable<Type> services, bool isSingleton)
    {
        Implementation = implementation;
        foreach (var service in services)
        {
            implementedServices.Add(service);
        }

        IsSingleton = isSingleton;
    }

    public static ImplementationCard FromType(Type type)
    {
        var implementedServices = type.GetInterfaces().Where(ServiceIndex.IsService);
        var isSingleton = type.IsAssignableTo(typeof(ISingletonService));
        return new(type, implementedServices, isSingleton);
    }
}
