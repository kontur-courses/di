using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloud
{
    public class Cloud : ICloud
    {
        public List<TagRectangle> Rectangles { get
            {
                var result = new List<TagRectangle>();
                foreach (var tag in config.TagCollection)
                    result.Add(new TagRectangle(tag.Text, layouter.PutNextRectangle(tag.Size,Center)));
                return result;
            } }
        public ClientData Data { get; }
        private Point Center { get { return new Point(Data.Width / 2, Data.Height / 2); } }
        private readonly ITagsConfig config;
        private readonly ICircularCloudLayouter layouter;
        public Cloud (ICircularCloudLayouter layouter, ITagsConfig config, IClientDataFactory factory)
        {
            Data = factory.CreateData();
            this.config = config;
            this.layouter = layouter;
        }
    }
}
