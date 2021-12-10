using System.Reflection;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer;

public class ServiceIndex
{
    private readonly HashSet<Type> availableServices = new();
    private readonly Dictionary<string, Type> nameServicePair = new();
    private readonly Dictionary<string, Type> fullNameServicePair = new();

    public IReadOnlySet<Type> AvailableServices => availableServices;

    public bool TryGetByName(string name, out Type type)
    {
        return nameServicePair.TryGetValue(name, out type!);
    }

    public bool TryGetByFullName(string fullName, out Type type)
    {
        return fullNameServicePair.TryGetValue(fullName, out type!);
    }

    public void AddAssemblyServices(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(x => x.IsClass && x.IsAssignableTo(typeof(IService)));
        foreach (var type in types)
        {
            availableServices.Add(type);
            nameServicePair[type.Name] = type;
            if (type.FullName != null)
                fullNameServicePair[type.FullName] = type;
        }
    }
}
