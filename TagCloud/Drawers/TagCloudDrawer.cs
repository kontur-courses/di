using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Layouters;

namespace TagCloud.Drawers
{
    public class TagCloudDrawer : ITagCloudDrawer
    {
        private IRectangleLayouter layouter;
        private IPicture picture;
        
        public TagCloudDrawer(IRectangleLayouter layouter, IPicture picture)
        {
            this.layouter = layouter;
            this.picture = picture;
        }

        public void DrawTagCloud(HashSet<TagInfo> tags)
        {
            MakePicture(GetTagsPositions(tags.ToList()));
        }
        
        private Dictionary<TagInfo, Point> GetTagsPositions(List<TagInfo> tags)
        {
            var tagsToPosition = new Dictionary<TagInfo, Point>();
            foreach (var tag in tags.OrderByDescending(t => t.Font.Height))
            {
                tagsToPosition[tag] = layouter.PutNextRectangle(picture
                    .MeasureStringSize(tag.Value, tag.Font)).Location;
            }

            return tagsToPosition;
        }
        
        private void MakePicture(Dictionary<TagInfo, Point> tagsToPosition)
        {
            picture.FillRectangle(new Rectangle(0, 0, 2000, 2000), Color.Black);
            
            foreach (var tagToPosition in tagsToPosition)
            {
                picture.DrawString(tagToPosition.Key.Value, tagToPosition.Key.Font,
                    tagToPosition.Value, Color.Yellow);
            }
            picture.Save();
        }
    }
}