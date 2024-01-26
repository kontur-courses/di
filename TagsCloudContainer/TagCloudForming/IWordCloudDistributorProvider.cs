using TagsCloudContainer.Common;

namespace TagsCloudContainer.TagCloudForming;

public interface IWordCloudDistributorProvider
{
    public IReadOnlyDictionary<string, WordData> DistributedWords { get; }
}