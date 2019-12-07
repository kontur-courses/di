using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Layouter
{
    class DefaultLayouterSettings : ILayouterSettings
    {
        public Point Center { get; } = new Point(0, 0);
    }
}
