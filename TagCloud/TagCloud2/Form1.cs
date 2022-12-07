using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TagCloud2
{
    public partial class Form1 : Form
    {
        private bool _alreadyDone;
        public Form1()
        {
            InitializeComponent();
            this.Width = 1280;
            this.Height = 768;
            Paint += new PaintEventHandler(Form1_Paint!);
        }

        //https://stackoverflow.com/a/13103960
        private void Print(Bitmap BM, PaintEventArgs e)
        {
            if (_alreadyDone)
                return;
            _alreadyDone = true;

            using (Graphics graphicsObj = e.Graphics)
                graphicsObj.DrawImage(BM, 0, 0);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            // Color.FromArgb(171, 207, 126),
            // Color.FromArgb(60, 90, 57),
            // Color.FromArgb(40, 63, 59)

            var maxFontSize = 30;
            var minFontSize = 4;

            var cloud = new SpiralTagCloud(
                new SpiralTagCloudEngine(new Point(Size / 2)),
                new SpiralTagCloudBitmapDrawer(
                    Size,
                    "Consolas",
                    maxFontSize,
                    minFontSize,
                    Color.FromArgb(252, 161, 125),
                    Color.FromArgb(186, 75, 134),
                    Color.FromArgb(13, 6, 40)
                    ),
                new DataParser(),
                minFontSize,
                maxFontSize
            );

            cloud.Parser.ParseFile("3.txt");

            // var sb = new StringBuilder();
            //
            // for (var i = 0; i < 101; i++)
            //     for (var j = 0; j < i; j++)
            //         sb.AppendLine(i.ToString());
            //
            // cloud.Parser.ParseText(sb.ToString());
            cloud.CreateTagCloud();


            //cloud.Drawer.DrawRectangles(cloud.Engine.Rectangles.ToArray());
            cloud.Drawer.DrawTags(
                cloud.Engine.Rectangles.ToArray(),
                cloud.TagWithSize.ToArray());
            Print(cloud.Drawer.Bitmap, e);
        }
    }
}