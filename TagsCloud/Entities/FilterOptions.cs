namespace TagsCloud.Entities;

public record FilterOptions(
    CaseType CaseType,
    bool CastWordsToInfinitive,
    List<string> ImportantTextParts,
    List<string> ExcludedWords);