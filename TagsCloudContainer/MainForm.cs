using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Settings;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, PictureBoxImageHolder pictureBox, ImageSettings settings)
        {
            ClientSize = new Size(settings.Width, settings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(settings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag Cloud Visualization";
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MainForm
            // 
            ClientSize = new System.Drawing.Size(284, 261);
            Name = "MainForm";
            Load += new System.EventHandler(this.MainForm_Load);
            ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}