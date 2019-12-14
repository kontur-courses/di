using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.TagClouds;
using TagCloudGenerator.Tags;

namespace TagCloudGenerator
{
    public class TagCloudContext
    {
        public string ImageName { get; }
        public Size ImageSize { get; }
        public IEnumerable<string> TagCloudContent { get; set; }
        public HashSet<string> ExcludedWords { get; }
        public TagCloud<TagType> Cloud { get; }
        public ICloudLayouter CloudLayouter { get; }

        public TagCloudContext(string imageName,
                               Size imageSize,
                               IEnumerable<string> tagCloudContent,
                               TagCloud<TagType> cloud,
                               ICloudLayouter cloudLayouter,
                               HashSet<string> excludedWords)
        {
            ImageName = imageName;
            ImageSize = imageSize;
            TagCloudContent = tagCloudContent;
            Cloud = cloud;
            CloudLayouter = cloudLayouter;
            ExcludedWords = excludedWords;
        }
    }
}