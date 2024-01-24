using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IFilterOptions
{
    public CaseType WordsCase { get; set; }
    public bool CastWordsToInfinitive { get; set; }
    public string[] ImportantLanguageParts { get; set; }
    public string[] ExcludedWords { get; set; }
}