using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.Filters;

public class LetterCaseFilter : FilterBase, IWordFilter
{
    public LetterCaseFilter(FilterOptions options) : base(options)
    {
    }

    public List<string> GetFilteredResult(List<string> words)
    {
        var caseType = Options.CaseType;

        if (caseType == CaseType.Default)
            return words;

        for (var i = 0; i < words.Count; i++)
            words[i] = caseType == CaseType.Upper ? words[i].ToUpper() : words[i].ToLower();

        return words;
    }
}