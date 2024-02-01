using Autofac.Features.AttributeFilters;
using TagsCloud.ConsoleCommands;
using TagsCloud.WordValidators;

namespace TagsCloud.TextReaders;

public class WordsProvider : IWordsProvider
{
    private readonly IWordValidator validator;
    private readonly string filename;

    public WordsProvider(IWordValidator validator, Options options)
    {
        this.validator = validator;
        this.filename = options.InputFile;
    }

    public Dictionary<string, int> GetWords()
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException(filename);
        return File.ReadAllText(filename)
            .Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None)
            .GroupBy(word => word.ToLower())
            .Where(g => validator.IsWordValid(g.Key))
            .OrderByDescending(g2 => g2.Count())
            .ToDictionary(group => group.Key, group => group.Count());
    }
}