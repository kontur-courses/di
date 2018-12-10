using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public class CloudSaver : ISaver<Bitmap>
    {
        private readonly Bitmap bitmapFile;

        public CloudSaver(IVisualizer visualizer)
        {
            this.bitmapFile = visualizer.Draw();
        }

        public void Save(string filename)
        {
            bitmapFile.Save(filename);
        }
    }
}
