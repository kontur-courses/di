using TagsCloudContainer.CloudBuilder;
using TagsCloudContainer.CloudDrawers;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.CloudTagController
{
    public class CloudTagController : ICloudTagController
    {
        private readonly IFileReader fileReader;
        private readonly ITextParser textParser;
        private readonly ICloudDrawer cloudDrawer;
        private readonly ICloudBuilder cloudBuilder;

        public CloudTagController(IFileReader fileReader, ITextParser textParser, ICloudDrawer cloudDrawer, ICloudBuilder cloudBuilder)
        {
            this.fileReader = fileReader;
            this.textParser = textParser;
            this.cloudDrawer = cloudDrawer;
            this.cloudBuilder = cloudBuilder;
        }

        public void Work()
        {
            cloudDrawer.Draw(cloudBuilder.BuildTagsCloud(textParser.Parse(fileReader.Read())));
        }
    }
}