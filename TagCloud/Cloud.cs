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
                foreach (var tag in tagCollection.Tags)
                    result.Add(new TagRectangle(tag.Text, layouter.PutNextRectangle(tag.Size,Center)));
                return result;
            } }
        public ClientData Data { get; }
        private Point Center => new Point(Data.Width / 2, Data.Height / 2);
        private readonly TagCollection tagCollection;
        private readonly ICircularCloudLayouter layouter;
        public Cloud (ICircularCloudLayouter layouter, ITagCollectionFactory tagCollectionFactory, IClientDataFactory ClientDataFactory)
        {
            Data = ClientDataFactory.CreateData();
            tagCollection = tagCollectionFactory.Create(Data.Path);
            this.layouter = layouter;
        }
    }
}
