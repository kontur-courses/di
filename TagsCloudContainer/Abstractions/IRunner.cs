using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface IRunner : IService
{
    void Run(params string[] args);
}
