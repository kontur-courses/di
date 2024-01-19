using System.Drawing;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.TagCloudForming;

public interface IWordCloudDistributorProvider
{
    public IReadOnlyDictionary<string, Word> DistributedWords { get; }
}