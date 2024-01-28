using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface IFilter
{
    void Apply(HashSet<WordTagGroup> wordGroups, IFilterOptions options);
}