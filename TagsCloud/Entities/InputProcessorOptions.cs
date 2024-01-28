using TagsCloud.Contracts;

namespace TagsCloud.Entities;

public class InputProcessorOptions : IInputProcessorOptions
{
    public bool OnlyRussian { get; init; }
    public bool ToInfinitive { get; init; }
    public CaseType WordsCase { get; init; }
    public HashSet<string> LanguageParts { get; init; } = new();
    public HashSet<string> ExcludedWords { get; init; } = new();
}