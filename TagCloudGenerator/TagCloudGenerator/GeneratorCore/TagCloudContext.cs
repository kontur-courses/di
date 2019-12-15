using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.GeneratorCore.CloudLayouters;
using TagCloudGenerator.GeneratorCore.TagClouds;

namespace TagCloudGenerator.GeneratorCore
{
    public class TagCloudContext
    {
        public string ImageName { get; }
        public Size ImageSize { get; }
        public IEnumerable<string> TagCloudContent { get; }
        public HashSet<string> ExcludedWords { get; }
        public ITagCloud Cloud { get; }
        public ICloudLayouter CloudLayouter { get; }

        public TagCloudContext(string imageName,
                               Size imageSize,
                               IEnumerable<string> tagCloudContent,
                               ITagCloud cloud,
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