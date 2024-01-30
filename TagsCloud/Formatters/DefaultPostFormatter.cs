using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.Formatters;

[Injection(ServiceLifetime.Singleton)]
public class DefaultPostFormatter : IPostFormatter
{
    public string Format(string input)
    {
        var trimmedInput = input.TrimStart();
        var endIdx = GetFirstNonLetterIndex(trimmedInput);
        
        return endIdx == -1 ? trimmedInput : trimmedInput[..endIdx];
    }

    private static int GetFirstNonLetterIndex(string line)
    {
        for (var i = 0; i < line.Length; i++)
            if (!char.IsLetter(line[i]))
                return i;

        return -1;
    }
}