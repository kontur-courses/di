using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class AlgorithmSettings
    {
        public double Dr { get; set; } = 0.01; // delta radius
        public double Fi { get; set; } = 0.0368; // angle
    }
}
