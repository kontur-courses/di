using System.Reflection;
using CommandLine;

namespace ConsoleApp.ArgumentsOptions;

public class HelpOption : IArgumentsOption
{
    public Type[] AvailableOptionsTypes { get; set; } = Array.Empty<Type>();

    [Option('h', "help", Required = false, HelpText = "Help")]
    public bool IsRequired { get; set; }

    public void Execute()
    {
        Console.WriteLine("Commands:");
        foreach (var optionsType in AvailableOptionsTypes.Concat(new[] { GetType() }))
        {
            var propertyInfos = optionsType.GetProperties()
                .Where(o => o.GetCustomAttribute(typeof(OptionAttribute)) is not null);
            foreach (var propertyInfo in propertyInfos)
            {
                if (Attribute.GetCustomAttribute(propertyInfo,
                        typeof(OptionAttribute)) is not OptionAttribute attribute)
                    continue;
                Console.WriteLine($"{attribute.HelpText} -{attribute.ShortName} --{attribute.LongName}");
            }
        }
    }
}