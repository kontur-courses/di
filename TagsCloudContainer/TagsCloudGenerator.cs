using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Visualizers;
using TagsCloudContainer.Readers;
using System.Drawing;

namespace TagsCloudContainer
{
    class TagsCloudGenerator
    {
        private ICloudLayouter cloudLayouter;
        private IWordCounter wordCounter;
        private IVisualizer visualizer;
        private IReader reader;

        public TagsCloudGenerator(
            ICloudLayouter cloudLayouter,
            IWordCounter wordCounter,
            IVisualizer visualizer,
            IReader reader)
        {
            this.cloudLayouter = cloudLayouter;
            this.wordCounter = wordCounter;
            this.visualizer = visualizer;
            this.reader = reader;
        }

        public Bitmap CreateTagCloud(string path)
        {
            throw new NotImplementedException();
        }
    }
}
