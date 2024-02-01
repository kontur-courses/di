using TagsCloudPainter.Settings;

namespace TagsCloudPainter.Parser;

public class BoringTextParser : ITextParser
{
    private static readonly string[] _separators = [" ", ". ", ", ", "; ", "-", "—", Environment.NewLine];
    private readonly ITextSettings textSettings;

    public BoringTextParser(ITextSettings textSettings)
    {
        this.textSettings = textSettings ?? throw new ArgumentNullException(nameof(textSettings));
    }

    public List<string> ParseText(string text)
    {
        var boringWords = GetBoringWords(textSettings.BoringText);
        var words = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        return words.Select(word => word.ToLower()).Where(word => !boringWords.Contains(word)).ToList();
    }

    public HashSet<string> GetBoringWords(string text)
    {
        var words = text.Split(Environment.NewLine);
        var boringWords = new HashSet<string>();

        foreach (var word in words)
            boringWords.Add(word.ToLower());

        return boringWords;
    }
}