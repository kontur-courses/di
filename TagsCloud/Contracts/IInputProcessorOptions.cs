using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IInputProcessorOptions : IFilterOptions
{
    bool ToInfinitive { get; init; }
    CaseType WordsCase { get; init; }
}