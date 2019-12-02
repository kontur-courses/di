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
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            Controls.Add(mainMenu);
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            //pictureBox.Size = new Size(600, 600);
            Controls.Add(pictureBox);
        }
    }
}
