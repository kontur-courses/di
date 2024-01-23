using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloud.Extensions;
using TagsCloud.Filters;

namespace TagsCloud;

public class Program
{
    [Option] public string Subject { get; set; }

    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    // Application entry point
    private void OnExecute()
    {
        var options = new FilterOptions
        {
            CaseType = CaseType.Default,
            ExcludedWords = new List<string>(),
            CastWordsToInfinitive = true,
            ImportantTextParts = new List<string>() { "S", "V" }
        };
        
        var services = new ServiceCollection();
        
        services.AddFilters();
        services.AddConveyor();
        services.AddFilterOptions(options);

        var provider = services.BuildServiceProvider();
        var conveyor = provider.GetService<FilterConveyor>();
    }
}