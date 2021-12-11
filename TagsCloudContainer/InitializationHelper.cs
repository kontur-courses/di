using Autofac;
using Mono.Options;
using System.Reflection;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer;

public static class InitializationHelper
{
    private const string allCase = "all";
    private const string noneCase = "none";

    public static void RunWithArgs(string[] args)
    {
        var registeredServices = new HashSet<Type>();
        var serviceIndex = new ServiceIndex();
        var builder = new ContainerBuilder();

        serviceIndex.AddAssemblyTypes(Assembly.GetExecutingAssembly());
        var leftArgs = ParseAssemblies(serviceIndex, args);
        leftArgs = ParseImplementations(serviceIndex, builder, registeredServices, leftArgs);
        RegisterNotSpecified(builder, serviceIndex, registeredServices);

        var container = builder.Build();

        var runner = container.Resolve<IRunner>();

        runner.Run(leftArgs.ToArray());
    }

    private static void RegisterNotSpecified(ContainerBuilder builder, ServiceIndex serviceIndex, HashSet<Type> registeredServices)
    {
        foreach (var impl in serviceIndex.AvailableImplementations)
        {
            var registration = builder.RegisterType(impl.Implementation).AsSelf();
            if (impl.IsSingleton)
                registration.SingleInstance();
            foreach (var possibleService in impl.ImplementedServices)
            {
                if (registeredServices.Contains(possibleService))
                    continue;
                registration.As(possibleService);
            }
        }
    }

    private static List<string> ParseImplementations(ServiceIndex serviceIndex, ContainerBuilder builder, HashSet<Type> registeredServices, List<string> leftArgs)
    {
        var implementationsOptions = new OptionSet()
        {
            {"implement-with=",$"Specifies which implementations to register for later use. " +
            $"Example: '--implement-with IService FirstImpl SecondImpl' will register class FirstImpl and SecondImpl as implementations for IService. " +
            $"Special cases: '{allCase}' - register all available implementations of service (default behaviour), '{noneCase}' - not register any implmentations for service" ,
                v => RegisterFromArgs(builder,serviceIndex, registeredServices, v) }
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
        if (argString == allCase)
            return;

        var types = argString.Split(' ');
        if (types.Length < 2)
            throw new ArgumentException($"{argString} did not provide enough types to register. Should be at least 2: Service and it's Implementation");
        var service = serviceIndex.GetService(types[0]);
        if (types[1] == noneCase)
        {
            registeredServices.Add(service);
            return;
        }

        var impls = types.Skip(1).Select(serviceIndex.GetImplementation).ToArray();
        Register(builder, service, impls);
        registeredServices.Add(service);
    }

    private static void Register(ContainerBuilder builder, Type service, ImplementationCard[] implemetations)
    {
        foreach (var impl in implemetations)
        {
            if (!service.IsAssignableFrom(impl.Implementation))
                throw new ArgumentException($"Implementation '{impl.Implementation.Name}' does not implement service '{service.Name}' interface");
            var registration = builder.RegisterType(impl.Implementation).AsSelf().As(service);
            if (impl.IsSingleton)
                registration.SingleInstance();
        }
    }

    private static void AddAssembliesFrom(ServiceIndex serviceIndex, IEnumerable<string> assemblyNames)
    {
        foreach (var assembly in assemblyNames.Select(Assembly.LoadFrom))
        {
            serviceIndex.AddAssemblyTypes(assembly);
        }
    }
}
