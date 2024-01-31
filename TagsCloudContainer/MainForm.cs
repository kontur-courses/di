using TagsCloudContainer.Infrastucture.Extensions;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] actions, PictureBox pictureBox, ImageSettings imageSetting)
        {
            ClientSize = new Size(imageSetting.Width, imageSetting.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSetting);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            InitializeComponent();
        }
    }
}
