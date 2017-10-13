using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class TagsCloudContainer : ITagsCloudContainer
    {
        private ITagsData tagsData;
        private ICircularCloudLayouter circularCloudLayouter;
        private ITagSizeNormalizer tagSizeNormalizer;

        private Dictionary<string, Rectangle> tagRectanglesData;

        public TagsCloudContainer(ITagsData tagsData, ICircularCloudLayouter circularCloudLayouter, ITagSizeNormalizer tagSizeNormalizer)
        {
            this.tagsData = tagsData;
            this.circularCloudLayouter = circularCloudLayouter;
            this.tagSizeNormalizer = tagSizeNormalizer;

            tagRectanglesData = new Dictionary<string, Rectangle>();
        }

        public Dictionary<string, Rectangle> GetTagsRectangleData()
        {
            foreach (var word in tagsData.GetData())
            {
                var size = tagSizeNormalizer.GetTagSize(word);
                var rectangle = circularCloudLayouter.PutNextRectangle(size);

                tagRectanglesData.Add(word, rectangle);
            }

            return tagRectanglesData;
        }
    }
}
