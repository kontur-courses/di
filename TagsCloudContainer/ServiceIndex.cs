using System.Reflection;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer;

public class ServiceIndex
{
    private static readonly HashSet<Type> serviceTypes = new()
    {
        typeof(IService),
        typeof(ISingletonService),
    };

    private readonly HashSet<ImplementationCard> availableImplementations = new();
    private readonly Dictionary<string, ImplementationCard> nameImplementationPair = new();
    private readonly Dictionary<string, ImplementationCard> fullNameImplementationPair = new();
    public IReadOnlySet<ImplementationCard> AvailableImplementations => availableImplementations;

    private readonly Dictionary<string, Type> nameServicePair = new();
    private readonly Dictionary<string, Type> fullNameServicePair = new();
    private readonly HashSet<Type> availableServices = new();
    public IReadOnlySet<Type> AvailableServices => availableServices;

    public ImplementationCard GetImplementation(string typeName)
    {
        if (!nameImplementationPair.TryGetValue(typeName, out var type))
        {
            if (!fullNameImplementationPair.TryGetValue(typeName, out type))
                throw new ArgumentException($"Could not find type with name '{typeName}'. Make sure required assembly is provided and requested service implements '{nameof(IService)}' interface");
        }

        return type;
    }

    public Type GetService(string typeName)
    {
        if (!nameServicePair.TryGetValue(typeName, out var type))
        {
            if (!fullNameServicePair.TryGetValue(typeName, out type))
                throw new ArgumentException($"Could not find type with name '{typeName}'. Make sure required assembly is provided and requested service implements '{nameof(IService)}' interface");
        }

        return type;
    }

    public void AddAssemblyTypes(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (IsService(type))
                AddService(type);
            else if (IsServiceImplementation(type))
                AddImplementation(type);
        }
    }

    private void AddImplementation(Type type)
    {
        var impl = ImplementationCard.FromType(type);
        availableImplementations.Add(impl);
        nameImplementationPair[impl.Implementation.Name] = impl;
        if (impl.Implementation.FullName != null)
            fullNameImplementationPair[impl.Implementation.FullName] = impl;
    }

    private void AddService(Type type)
    {
        availableServices.Add(type);
        nameServicePair[type.Name] = type;
        if (type.FullName != null)
            fullNameServicePair[type.FullName] = type;
    }

    public static bool IsServiceImplementation(Type type)
    {
        return type.IsClass && IsServiceLike(type);
    }

    private static bool IsServiceLike(Type type)
    {
        return serviceTypes.Any(x => type.IsAssignableTo(x)) && serviceTypes.All(x => type != x);
    }

    public static bool IsService(Type type)
    {
        return type.IsInterface && IsServiceLike(type);
    }
}
