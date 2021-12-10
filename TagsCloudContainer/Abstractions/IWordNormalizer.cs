using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface IWordNormalizer : IService
{
    string? Normalize(string word);
}