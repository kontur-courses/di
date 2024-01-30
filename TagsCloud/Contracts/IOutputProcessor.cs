using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface IOutputProcessor
{
    void SaveVisualization(HashSet<WordTagGroup> wordGroups, string filename);
}