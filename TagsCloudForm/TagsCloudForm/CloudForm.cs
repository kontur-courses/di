using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TagsCloudVisualization;

namespace TagsCloudForm
{
    class CloudForm : Form
    {
        private readonly int  RectanglesNum;
        private Point CloudCenter;
        private readonly int MinRectSize;
        private readonly int MaxRectSize;
        private readonly CircularCloudLayouter Layouter;


        public CloudForm()
        {
            InitializeComponent();
            CloudCenter = new Point(
                (int)Math.Floor(Size.Width / (double)2),
                (int)Math.Floor(Size.Height / (double)2));
            Layouter = new CircularCloudLayouter(CloudCenter);
            RectanglesNum = 10;
        }

        public CloudForm(int rectanglesNum, int minRectSize, int maxRectSize)
        {
            InitializeComponent();
            CloudCenter = new Point(
                (int)Math.Floor(Size.Width / (double)2),
                (int)Math.Floor(Size.Height / (double)2));
            Layouter = new CircularCloudLayouter(CloudCenter);
            RectanglesNum = rectanglesNum;
            MinRectSize = minRectSize;
            MaxRectSize = maxRectSize;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Random rnd = new Random();
            var graphics = e.Graphics;
            var rectangles = new List<Rectangle>();
            var rectBorderPen = new Pen(Color.Black, 2);
            var rectFillBrush = new SolidBrush(Color.LightGreen);
            for (int i = 0; i < RectanglesNum; i++)
            {
                var rect = Layouter.PutNextRectangle(new Size(rnd.Next(MinRectSize, MaxRectSize), rnd.Next(MinRectSize, MaxRectSize)));
                graphics.FillRectangle(rectFillBrush, rect);
                graphics.DrawRectangle(rectBorderPen, rect);
                rectangles.Add(rect);
                Thread.Sleep(100);
            }

        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // CloudForm
            // 
            ClientSize = new Size(584, 561);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "CloudForm";
            ResumeLayout(false);

        }
    }
}
