using TagsCloudVisualization;

namespace CloudContainer.ConfigCreators
{
    public interface IConfigCreator
    {
        IWordConfig CreateConfig(string[] args);
    }
}