namespace TagCloud.Domain.Settings;

public class WordSettings
{
    public List<string> Excluded { get; set; } = new()
    {
        "б****",
        "п****"
    };
}