using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IFilterOptions
{
    CaseType        WordsCase     { get; set; }
    bool            ToInfinitive  { get; set; }
    HashSet<string> LanguageParts { get; set; }
    HashSet<string> ExcludedWords { get; set; }
}