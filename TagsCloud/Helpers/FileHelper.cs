using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.Helpers;

public static class FileHelper
{
    public static readonly char[] Separators = { ' ', '=', ';', ',', '.', ':', '!', '?' };

    public static List<string> GetLinesFromFile(string filename)
    {
        var fileExtension = filename.Split('.', StringSplitOptions.RemoveEmptyEntries)[^1];
        var reader = FindAppropriateReader(fileExtension);

        if (reader == null)
            throw new NotSupportedException("Unknown file extension!");

        var lines = reader
            .ReadContent(filename)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim())
            .ToList();

        return RemoveExcess(lines);
    }

    private static IFileReader? FindAppropriateReader(string fileExtension)
    {
        var readerType = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.GetInterfaces().Any(inter => inter == typeof(IFileReader)))
            .Where(type => Attribute.IsDefined(type, typeof(SupportedExtensionAttribute)))
            .FirstOrDefault(type =>
                type.GetCustomAttribute<SupportedExtensionAttribute>()!.FileExtension.Equals(fileExtension));

        if (readerType == null)
            return null;

        return (Activator.CreateInstance(readerType) as IFileReader)!;
    }

    private static List<string> RemoveExcess(List<string> lines)
    {
        for (var i = 0; i < lines.Count; i++)
        {
            if (!Separators!.Any(sep => lines[i].Contains(sep)))
                continue;

            lines[i] = lines[i].Split(Separators, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        return lines;
    }
}