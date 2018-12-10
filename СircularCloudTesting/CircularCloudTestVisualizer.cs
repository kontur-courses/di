using System.Drawing;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace СircularCloudTesting
{
    public class CircularCloudTestVisualizer
    {

        public Bitmap DrawCircularCloud(CircularCloudLayouter cloud)
        {
            var bmp = new Bitmap(cloud.WindowSize.Width, cloud.WindowSize.Height);
            var gr = Graphics.FromImage(bmp);
            gr.Clear(Color.AliceBlue);
            var pen = new Pen( Color.BlueViolet,1);
            cloud.Rectangles.ForEach(rect => gr.DrawRectangle(pen, rect));
            return bmp;
        }
    }
}