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
                Location = new Point(0, 0),
                Size = ClientSize
            };
            Controls.Add(pictureBox);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ShowImageForm
            // 
            ClientSize = new Size(282, 253);
            Name = "ShowImageForm";
            Load += ShowImageForm_Load;
            ResumeLayout(false);
        }

        private void ShowImageForm_Load(object sender, EventArgs e)
        {
        }
    }
}