using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface IStyler : IService
{
    IStyledTag Style(ITag source);
}