namespace TagsCloud.Entities;

public class FilterOptions
{
    public CaseType CaseType { get; init; }
    public bool CastWordsToInfinitive { get; init; }
    public List<string> ImportantTextParts { get; init; }
    public List<string> ExcludedWords { get; init; }
}