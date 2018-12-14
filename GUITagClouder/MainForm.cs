using System;
using System.Drawing;
using System.Windows.Forms;
using ConsoleTagClouder;

namespace FractalPainting.App
{
    public class MainForm : Form
    {
        public MainForm(AppSettings settings, PictureBoxImageHolder pictureBox, IUiAction[] actions)
        {
            ClientSize = new Size(100,100);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}