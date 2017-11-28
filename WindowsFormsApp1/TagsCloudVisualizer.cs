using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class TagsCloudVisualizer
    {
        private ICloudVisualizer Visualizer { get; }
        private ICloudCombiner CloudCombiner { get; }
        public TagsCloudVisualizer(ICloudVisualizer visualizer, ICloudCombiner cloudCombiner)
        {
            Visualizer = visualizer;
            CloudCombiner = cloudCombiner;
        }

        public void View()
        {
            var cloud = CloudCombiner.GetCloud();
            Visualizer.DrawCloud(cloud);
        }
    }
}