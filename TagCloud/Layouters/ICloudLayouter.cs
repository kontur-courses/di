using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Layouters
{
    public interface ICloudLayouter
    {
        //Rectangle PutNextRectangle(Size rectangleSize);
        IEnumerable<Tag> PutTags(IEnumerable<Tag> tags);
    }
}
