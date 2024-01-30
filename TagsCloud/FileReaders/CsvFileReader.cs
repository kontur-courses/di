using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;

namespace TagsCloud.FileReaders;

[Injection(ServiceLifetime.Singleton)]
public class CsvFileReader : IFileReader
{
    public string SupportedExtension => "csv";

    public IEnumerable<string> ReadContent(string filename, IPostFormatter postFormatter = null)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        using var reader = new StreamReader(filename);
        using var csv = new CsvReader(reader, configuration);

        foreach (var cell in csv.GetRecords<TableCell>())
            yield return postFormatter is null ? cell.Word : postFormatter.Format(cell.Word);
    }
}