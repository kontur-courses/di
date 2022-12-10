using System.Drawing.Printing;
using System.Windows.Forms;
using TagsCloudContainer.Actions;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] actions, PictureBox pictureBox, ImageSettings imageSettings)
        {
            Size = new Size(imageSettings.Width, imageSettings.Height);

            var menu = new MenuStrip();
            menu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(menu);
            
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            InitializeComponent();
        }
    }
}