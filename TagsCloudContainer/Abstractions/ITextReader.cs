using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface ITextReader : IService
{
    IEnumerable<string> ReadLines();
}