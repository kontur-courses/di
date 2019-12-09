using System.Drawing;
using System.Web.UI.WebControls;

namespace TagCloud.Models
{
    public class TagRectangle
    {
        public RectangleF Area {get;}
        public Tag Tag { get; }
        public TagRectangle(Tag tag,RectangleF area)
        {
            Area = area;
            Tag = tag;
        }
        public TagRectangle()
        {
            Area = new RectangleF();
            Tag = null;
        }
    }   
}
