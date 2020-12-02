using TagsCloudVisualization;

namespace CloudContainer.ConfigCreators
{
    public interface IConfigCreator
    {
        void CreateConfig(string[] args, IConfig config);
    }
}