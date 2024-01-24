using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Conveyors;
using TagsCloud.Entities;
using TagsCloud.Extensions;
using TagsCloud.Helpers;

namespace TagsCloud;

public class Program
{
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    private void OnExecute()
    {
        // TODO: form this options from user input
        var options = new FilterOptions(
            CaseType.Lower, 
            true, 
            new List<string> { "CONJ" },
            new List<string>());

        var provider = new ServiceCollection()
            .AddFiltersWithOptions(options)
            .BuildServiceProvider();

        var lines = FileHelper.GetLinesFromFile("/home/luvairo/textdata.txt");
        
        var conveyor = provider.GetRequiredService<FilterConveyor>();
        conveyor.ApplyFilters(lines);

        Console.WriteLine();
    }
}