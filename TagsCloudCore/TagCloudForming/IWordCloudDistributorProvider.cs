using TagsCloudCore.Common;

namespace TagsCloudCore.TagCloudForming;

public interface IWordCloudDistributorProvider
{
    public IReadOnlyDictionary<string, WordData> DistributedWords { get; }
}