using TagCloudGenerator.GeneratorCore.TagClouds;

namespace TagCloudGenerator.Clients
{
    public interface IClient
    {
        ITagCloudOptions<ITagCloud> GetOptions();
    }
}