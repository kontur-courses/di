using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Autofac;

namespace TagsCloudForm
{
    class CloudForm : Form
    {

        public CloudForm(IUiAction[] actions, PictureBoxImageHolder pictureBox,
            ImageSettings imageSettings)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            Controls.Add(mainMenu);
            InitializeComponent();
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
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
