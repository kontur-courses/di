using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public class CircularCloudLayouterSettings
    {
        public int CenterX { get; set; } = 40;

        public int CenterY { get; set; } = 40;

        public int MinSize { get; set; } = 10;

        public int MaxSize { get; set; } = 30;

        public int IterationsCount { get; set; } = 5;
    }
}
