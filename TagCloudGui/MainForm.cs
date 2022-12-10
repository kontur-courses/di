using System;
using System.Drawing;
using System.Windows.Forms;
using TagCloud.TagCloudVisualizations;
using TagCloudGui.Infrastructure;
using TagCloudGui.Infrastructure.Extensions;

namespace TagCloudGui
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, 
            PictureBoxImageHolder pictureBox,
            ITagCloudVisualizationSettings settings)
        {
            if (settings.PictureSize.HasValue)
                ClientSize = new Size(settings.PictureSize.Value.Width, settings.PictureSize.Value.Height);
            else
                settings.PictureSize = ClientSize; 
            
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
            Text = "Tag Cloud";
        }
    }
}
