using TagsCloudVisualization;

namespace CloudContainer.ConfigCreators
{
    public interface IConfigCreator
    {
        IConfig CreateConfig(string[] args);
    }
}