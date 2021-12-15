using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloud
    {
        private readonly FileReader fileReader;
        private readonly TokenGenerator tokenGenerator;
        private readonly TagCloudMaker tagCloudMaker;
        private readonly TagCloudVisualiser tagCloudVisualiser;

        public TagCloud(FileReader fileReader, TokenGenerator tokenGenerator,
            TagCloudMaker tagCloudMaker, TagCloudVisualiser tagCloudVisualiser)
        {
            this.fileReader = fileReader;
            this.tagCloudMaker = tagCloudMaker;
            this.tagCloudVisualiser = tagCloudVisualiser;
            this.tokenGenerator = tokenGenerator;
        }

        public void CreateTagCloudFromFile(FileInfo source, Font font, int maxTagCount,
           Size resolution, string resultPath, ImageFormat format)
        {
            if (!fileReader.CanReadFile(source))
                throw new ArgumentException("Unknown source file format.");
            var text = fileReader.ReadFile(source);
            var tokens = tokenGenerator.GetTokens(text, maxTagCount).ToArray();
            var tags = tagCloudMaker.CreateTagCloud(tokens, font);
            if (tags.Length == 0)
                throw new ArgumentException("Zero tags found.");
            var image = tagCloudVisualiser.Render(tags, resolution);
            image.Save(resultPath, format);
        }
    }
}