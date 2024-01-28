using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.FileReaders;

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
            yield return cell.Word;
    }
}