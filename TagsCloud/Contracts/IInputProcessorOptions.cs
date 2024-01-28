using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IInputProcessorOptions : IFilterOptions
{
    bool ToInfinitive { get; }
    CaseType WordsCase { get; }
}