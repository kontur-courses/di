using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.LayoutGeneration
{
    public class TagCloud : ITagsCloud
    {
        public List<Rectangle> Rectangles { get; set; }
        
        public TagCloud (List<Rectangle> rectangles)
        {
            Rectangles = rectangles;
        }
        
        public TagCloud()
        {
            this.Rectangles = new List<Rectangle>();
        }
    }
}