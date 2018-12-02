using System;
using System.Drawing;
using System.Windows.Forms;
using CloudLayouter.Infrastructer;
using CloudLayouter.Infrastructer.Common;
using CloudLayouter.Infrastructer.Extensions;

namespace CloudLayouter
{
    public class MainForm : Form
    {
        
        
        public MainForm(IUiAction[] actions,PictureBoxImageHolder pictureBox, ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);


            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            Text = "Cloud Layouter";
            base.OnShown(e);
        }
    }
}