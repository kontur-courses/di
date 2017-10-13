using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Implementation
{
    class TagsCloudSaver : ITagCloudSaver
    {
        private readonly ITagsCloudVisualizator tagsCloudVisualizator;
        private readonly string fileName;
        private readonly ImageFormat imageFormat;

        public TagsCloudSaver(ITagsCloudVisualizator tagsCloudVisualizator, string fileName, ImageFormat imageFormat)
        {
            this.tagsCloudVisualizator = tagsCloudVisualizator;
            this.fileName = fileName;
            this.imageFormat = imageFormat;
        }

        public void Save()
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            var image = tagsCloudVisualizator.GetTagsCloudImage();
            image.Save(fileName, imageFormat);
        }
    }
}
