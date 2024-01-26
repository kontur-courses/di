using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.Helpers;

public static class FileHelper
{
    private static readonly char[] separators = { ' ', '=', ';', ',', '.', ':', '!', '?' };

    public static List<string> GetLinesFromFile(string filename)
    {
        var fileExtension = filename.Split('.', StringSplitOptions.RemoveEmptyEntries)[^1];
        var reader = FindAppropriateReader(fileExtension);

        if (reader == null)
            throw new NotSupportedException("Unknown file extension!");

        var lines = reader.ReadContent(filename).Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

        return RemoveExcess(lines);
    }

    private static IFileReader FindAppropriateReader(string fileExtension)
    {
        var readerType = Assembly.GetExecutingAssembly().GetTypes().Where(IsCorrectFileReaderType)
            .FirstOrDefault(type =>
                type.GetCustomAttribute<SupportedExtensionAttribute>()!.FileExtension.Equals(fileExtension));

        if (readerType == null)
            return null;

        return (Activator.CreateInstance(readerType) as IFileReader)!;
    }

    private static bool IsCorrectFileReaderType(Type readerType)
    {
        return readerType.IsClass && readerType.GetInterfaces().Any(inter => inter == typeof(IFileReader)) &&
               Attribute.IsDefined(readerType, typeof(SupportedExtensionAttribute));
    }

    private static List<string> RemoveExcess(List<string> lines)
    {
        for (var i = 0; i < lines.Count; i++)
            lines[i] = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries)[0];

        return lines;
    }
}