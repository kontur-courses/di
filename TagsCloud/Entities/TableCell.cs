using CsvHelper.Configuration.Attributes;

namespace TagsCloud.Entities;

public class TableCell
{
    [Index(0)]
    public string Word { get; set; }
}