using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Palettes;

namespace TagsCloudContainer.Visualizers
{
    class SimpleVsualizer : IVisualizer
    {
        private IPalette palette;

        public SimpleVsualizer(IPalette palette)
        {
            this.palette = palette;
        }

        public Bitmap VisualizeCloud(List<TagToken> tags)
        {
            throw new NotImplementedException();
        }
    }
}
