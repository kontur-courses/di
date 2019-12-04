using System.Drawing;
using System.Web.UI.WebControls;

namespace TagCloud.Models
{
    public class TagRectangle
    {
        public Rectangle Area {get;}
        public string Text { get; }
        public FontSize FSize { get; }
        public TagRectangle(string text,Rectangle area,FontSize fSize)
        {
            FSize = fSize;
            Area = area;
            Text = text;
        }
    }   
}
