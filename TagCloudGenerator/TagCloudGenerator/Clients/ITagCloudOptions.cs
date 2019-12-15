using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.GeneratorCore.TagClouds;
using TagCloudGenerator.GeneratorCore.Tags;

namespace TagCloudGenerator.Clients
{
    public interface ITagCloudOptions<out TTagCloud> where TTagCloud : ITagCloud
    {
        string CloudVocabularyFilename { get; }
        string ImageSize { get; }
        string ExcludedWordsVocabularyFilename { get; }
        string ImageFilename { get; }
        int GroupsCount { get; }
        string MutualFont { get; }
        string BackgroundColor { get; }
        string FontSizes { get; }
        string TagColors { get; }

        TTagCloud ConstructCloud(Color backgroundColor, Dictionary<TagType, TagStyle> tagStyleByTagType);
    }
}