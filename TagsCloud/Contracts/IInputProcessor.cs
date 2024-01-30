using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface IInputProcessor
{
    HashSet<WordTagGroup> CollectWordGroupsFromFile(string filename);
}