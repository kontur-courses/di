using System.Drawing;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.TagClouds;

namespace TagCloudGenerator
{
    public class TagCloudContext
    {
        public string ImageName { get; }
        public Size ImageSize { get; }
        public string[] TagCloudContent { get; }
        public TagCloud Cloud { get; }
        public ICloudLayouter CloudLayouter { get; }

        public TagCloudContext(
            string imageName, Size imageSize, string[] tagCloudContent, TagCloud cloud, ICloudLayouter cloudLayouter)
        {
            ImageName = imageName;
            ImageSize = imageSize;
            TagCloudContent = tagCloudContent;
            Cloud = cloud;
            CloudLayouter = cloudLayouter;
        }
    }
}