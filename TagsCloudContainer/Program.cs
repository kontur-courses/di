using Autofac;
using Mono.Options;
using System.Reflection;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer;
public class Program
{
    public static void Main(string[] args)
    {
        args = new[] { "--files", "bigbook.txt", "--height", "10000", "--width", "10000", "--center", "5000, 5000", "--word-limit", "3000", "--color", "red" };
        var registeredServices = new HashSet<Type>();
        var serviceIndex = new ServiceIndex();
        var builder = new ContainerBuilder();

        serviceIndex.AddAssemblyServices(Assembly.GetExecutingAssembly());
        var leftArgs = ParseAssemblies(serviceIndex, args);
        leftArgs = ParseImplementations(serviceIndex, builder, registeredServices, leftArgs);

        foreach (var type in serviceIndex.AvailableServices)
        {
            var registration = builder.RegisterType(type).AsSelf();
            if (type.IsAssignableTo<ISingletonService>())
                registration.SingleInstance();
            foreach (var possibleService in type.GetInterfaces().Where(x => x.IsAssignableTo<IService>() && x != typeof(IService) && x != typeof(ISingletonService)))
            {
                if (registeredServices.Contains(possibleService))
                    continue;
                registration.As(possibleService);
            }
        }
        var container = builder.Build();

        var runner = container.Resolve<IRunner>();

        runner.Run(leftArgs.ToArray());
    }

    private static List<string> ParseImplementations(ServiceIndex serviceIndex, ContainerBuilder builder, HashSet<Type> registeredServices, List<string> leftArgs)
    {
        var implementationsOptions = new OptionSet()
        {
            {"implement-with=",$"Specifies which implementations to register for later use. " +
            $"Example: '--implement-with IService FirstImpl SecondImpl' will register class FirstImpl and SecondImpl as implementations for IService. " +
            $"'--implement-with IService all' will register all available implementations. " +
            $"All services without specific implemetation will register all available implementations", v => RegisterFromArgs(builder,serviceIndex, registeredServices, v) }
        };
        leftArgs = implementationsOptions.Parse(leftArgs);
        return leftArgs;
    }

    private static List<string> ParseAssemblies(ServiceIndex serviceIndex, string[] args)
    {
        var assembliesOptions = new OptionSet()
        {
            {"assemblies=",$"Specifies additional assemblies to use.",v => AddAssembliesFrom(serviceIndex,v.Split()) }
        };
        var leftArgs = assembliesOptions.Parse(args);
        return leftArgs;
    }

    private static void RegisterFromArgs(ContainerBuilder builder, ServiceIndex serviceIndex, HashSet<Type> registeredServices, string argString)
    {
        var types = argString.Split(' ');
        if (types.Length < 2)
            throw new ArgumentException($"{argString} did not provide enough types to register. Should be at least 2: Service and it's Implementation");
        var service = GetType(serviceIndex, types[0]);
        var impls = types.Skip(1).Select(x => GetType(serviceIndex, x)).ToArray();
        Register(builder, service, impls);
        registeredServices.Add(service);
    }

    private static Type GetType(ServiceIndex serviceIndex, string typeName)
    {
        if (!serviceIndex.TryGetByName(typeName, out var type))
        {
            if (!serviceIndex.TryGetByFullName(typeName, out type))
                throw new ArgumentException($"Could not find type with name '{typeName}'. Make sure required assembly is provided and requested service implements '{nameof(IService)}' interface");
        }

        return type;
    }

    private static void Register(ContainerBuilder builder, Type service, Type[] implemetations)
    {
        foreach (var impl in implemetations)
        {
            if (!service.IsAssignableFrom(impl))
                continue;
            var registration = builder.RegisterType(impl).AsSelf().As(service);
            if (impl.IsAssignableTo<ISingletonService>())
                registration.SingleInstance();
        }
    }

    private static void AddAssembliesFrom(ServiceIndex serviceIndex, IEnumerable<string> assemblyNames)
    {
        foreach (var assembly in assemblyNames.Select(Assembly.LoadFrom))
        {
            serviceIndex.AddAssemblyServices(assembly);
        }
    }
}
