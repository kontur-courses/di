using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Visualisator
{
    public interface IPainter
    {
        public void Paint(List<(Rectangle rectangle, string text)> rectangles);
    }
}
