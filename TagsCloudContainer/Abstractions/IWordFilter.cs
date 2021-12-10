using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface IWordFilter : IService
{
    bool IsValid(string word);
}