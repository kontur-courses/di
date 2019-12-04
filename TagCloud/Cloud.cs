using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloud
{
    public class Cloud : ICloud
    {
        private readonly ITagCollectionFactory tagCollectionFactory;
        private readonly ICircularCloudLayouter layouter;
        public Cloud (ICircularCloudLayouter layouter, ITagCollectionFactory tagCollectionFactory)
        {
            this.tagCollectionFactory = tagCollectionFactory;
            this.layouter = layouter;
        }

        public List<TagRectangle> GetRectangles(int width,int height,string path = null)
        {
            var tagCollection = tagCollectionFactory.Create(path);
            var center = new Point(width/2,height/2);
            var rectangles = tagCollection.Tags
                .Select(t => new TagRectangle(t.Text, layouter.PutNextRectangle(t.Size, center),t.FSize))
                .ToList();
            layouter.Clear();
            return rectangles;
        }
    }
}
