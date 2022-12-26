using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudContainer.Models;

namespace TagCloudContainer.Interfaces
{
    public interface ITagCloud
    {
        public void CreateTagCloud(IPointProvider pointFigure, IRectangleBuilder rectangleBuilder, IEnumerable<ITag> tags);
        public List<RectangleWithText> GetRectangles();
    }
}
