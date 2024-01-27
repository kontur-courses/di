namespace TagsCloud.Contracts;

public interface IFilterOptions
{
    HashSet<string> LanguageParts { get; init; }
    HashSet<string> ExcludedWords { get; init; }
}