using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.InfrastructureUI;

namespace TagsCloudVisualization
{
    public class CloudForm : Form
    {
        
        public CloudForm(IEnumerable<IUiAction> actions, PictureBoxImageHolder pictureBox)
        {
            var imageSettings = new ImageSettings();
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            pictureBox.RecreateImage(imageSettings);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToArray().ToMenuItems());
            Controls.Add(mainMenu);

            
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }


        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    var font = new Font("Bahnschrift", 12);
        //    var randon = new Random();
        //    var z = new FontSettings();
        //    var a = new DefinerSize(ClientSize, z).DefineFontSize( dictionary);
        //    var g = e.Graphics;
        //    var cloud = new Cloud(new Spiral(1, new Point(ClientSize.Width /2, ClientSize.Height / 2)));
        //    foreach (var word in a.Keys.OrderBy(w => dictionary[w]).Reverse())
        //    {
        //        var b = TextRenderer.MeasureText(word, a[word]);
        //        var r = cloud.PutNextRectangle(new Size(b.Width + 1, b.Height + 1));
        //        var r1 = new RectangleF(r.X, r.Y, r.Width + 1, r.Height + 1);
        //        var drawFormat = new StringFormat { Alignment = StringAlignment.Center};
        //        g.DrawString(word, a[word], new SolidBrush(Color.FromArgb(randon.Next(1, 255), randon.Next(1, 255), randon.Next(1, 255))), r1, drawFormat);
        //    }
        //}


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Cloud painter";
        }
    }




}