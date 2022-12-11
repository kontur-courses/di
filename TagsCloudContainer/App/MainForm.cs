using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {

        public MainForm(IUiAction[] actions, PictureBoxImageHolder pictureBox,
            ImageSettings imageSettings)
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
            base.OnShown(e);
            Text = "Tags cloud visualization";
        }
    }
}
