using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class TagsCloudContainer
    {
        private ITagsData tagsData;
        private ICircularCloudLayouter circularCloudLayouter;

        private Dictionary<string, Rectangle> tagRectanglesData;

        public TagsCloudContainer(ITagsData tagsData, ICircularCloudLayouter circularCloudLayouter)
        {
            this.tagsData = tagsData;
            this.circularCloudLayouter = circularCloudLayouter;

            tagRectanglesData = new Dictionary<string, Rectangle>();
        }

        public Dictionary<string, Rectangle> GetTagsRectangleData()
        {
            foreach (var word in tagsData.GetData())
            {
                var size = new Size();
                var rectangle = circularCloudLayouter.PutNextRectangle(size);

                tagRectanglesData.Add(word, rectangle);
            }

            return tagRectanglesData;
        }






    }
}
