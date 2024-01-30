using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface ICloudProcessor
{
    void SetPositions(HashSet<WordTagGroup> wordGroups);
    void SetFonts(HashSet<WordTagGroup> wordGroups);
    void SetColors(HashSet<WordTagGroup> wordGroups);
}