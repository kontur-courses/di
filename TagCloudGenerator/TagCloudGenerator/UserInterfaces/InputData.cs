using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.Tags;

namespace TagCloudGenerator.UserInterfaces
{
    public class InputData
    {
        public InputData(IEnumerable<string> cloudVocabulary,
                         Size imageSize,
                         string imageFilename,
                         Color backgroundColor,
                         Dictionary<TagType, TagStyle> tagStyleByTagType)
        {
            CloudVocabulary = cloudVocabulary;
            ImageSize = imageSize;
            ImageFilename = imageFilename;
            BackgroundColor = backgroundColor;
            TagStyleByTagType = tagStyleByTagType;
        }
        
        public IEnumerable<string> CloudVocabulary { get; }
        public Size ImageSize { get; }
        public string ImageFilename { get; }
        public Color BackgroundColor { get; }
        public Dictionary<TagType, TagStyle> TagStyleByTagType { get; }
    }
}