using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public interface IWordStatisticMaker
    {
        IEnumerable<TagStatistic> GetStatistic(IEnumerable<string> words);
    }
    public class CountingWordStatistic : IWordStatisticMaker
    {
        public IEnumerable<TagStatistic> GetStatistic(IEnumerable<string> words)
        {
            return words.GroupBy(x => x, x => x).Select(x => new TagStatistic(x.Key, x.ToList().Count));
        }
    }

    public class WinFormCloudVisualizer : Form , ICloudVisualizer
    {
        public void DrawCloud(Cloud cloud)
        {
            Width = 600;
            Height = 600;
            var pb = new PictureBox
            {

                Width = this.Width,
                Height = this.Height,
            };
            var drawArea = new Bitmap(pb.Size.Width, pb.Size.Height);
            Controls.Add(pb);


            using (var g = Graphics.FromImage(drawArea))
            {
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoClip;
                foreach (var word in cloud.Words)
                {
                    var font = new Font("Arial", word.FontSize, FontStyle.Bold, GraphicsUnit.Point);
                    g.DrawString(word.Value,font,Brushes.Red, word.Area,sf);
                }
            }
            pb.Image = drawArea;
            pb.Invalidate();
            Application.Run(this);
        }
    }
}