using System;
using System.Drawing;
using System.Windows.Forms;

namespace TagCloud
{
    public class ShowImageForm : Form
    {
        public ShowImageForm(Bitmap image)
        {
            ClientSize = new Size(image.Width, image.Height);
            var pictureBox = new PictureBox
            {
                Image = image,
                ClientSize = new Size(image.Width, image.Height),
                Location = new Point(0,0)
            };
            Controls.Add(pictureBox);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShowImageForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "ShowImageForm";
            this.Load += new System.EventHandler(this.ShowImageForm_Load);
            this.ResumeLayout(false);

        }

        private void ShowImageForm_Load(object sender, EventArgs e)
        {

        }
    }
}
