using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface ITextAnalyzer : IService
{
    ITextStats AnalyzeText();
}