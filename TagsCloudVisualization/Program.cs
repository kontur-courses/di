using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Algorithm.Curves;
using TagsCloudVisualization.Drawer;

namespace TagsCloudVisualization
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var size = new Size(1500, 700);
            var center = new Point(size.Width / 2, size.Height / 2);
            var spiral = new Spiral(1, center);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CloudForm(size, GetRectangles(300, spiral), new CloudDrawer(Color.Black)));
        }
        private static RectangleF[] GetRectangles(int count, ICurve curve)
        {
            var random = new Random();
            var cloud = new Cloud(curve);
            var rectangles = new List<RectangleF>();
            for (var i = 0; i < count; i++)
            {
                var r = cloud.PutNextRectangle(new Size(random.Next(20, 40), random.Next(30, 30)));
                rectangles.Add(new RectangleF(r.X, r.Y, r.Width, r.Height));
            }

            return rectangles.ToArray();
        }
    }


}
