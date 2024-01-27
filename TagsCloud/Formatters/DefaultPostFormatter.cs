using TagsCloud.Contracts;

namespace TagsCloud.Formatters;

public class DefaultPostFormatter : IPostFormatter
{
    private static readonly char[] separators = { ' ', '=', ';', ',', '.', ':', '!', '?' };

    public string Format(string input)
    {
        return input.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
    }
}