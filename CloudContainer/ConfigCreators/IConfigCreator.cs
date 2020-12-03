using TagsCloudVisualization.Configs;

namespace CloudContainer.ConfigCreators
{
    public interface IConfigCreator
    {
        void CreateConfig(IConfig config, Arguments arguments);
    }
}