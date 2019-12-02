using System.Windows.Forms;
using TagCloud;
using TagCloudForm.Actions;
using TagCloudForm.Holder;

namespace TagCloudForm
{
    public partial class TagCloudForm : Form
    {
        public TagCloudForm(IUiAction[] actions, PictureBoxImageHolder pictureBox, ImageSettings imageSettings)
        {
            InitializeComponent();
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
            pictureBox.RecreateImage(imageSettings);
        }
    }
}