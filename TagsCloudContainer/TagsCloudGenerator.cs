using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Visualizers;

namespace TagsCloudContainer
{
    class TagsCloudGenerator
    {
        private ICloudLayouter cloudLayouter;
        private IWordCounter wordCounter;
        private IVisualizer visualizer;

        public TagsCloudGenerator(ICloudLayouter cloudLayouter, IWordCounter wordCounter, IVisualizer visualizer)
        {
            this.cloudLayouter = cloudLayouter;
            this.wordCounter = wordCounter;
            this.visualizer = visualizer;
        }
    }
}
