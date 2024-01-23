using TagsCloudPainterApplication.Actions;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication;

public partial class MainForm : Form
{
    public MainForm(IUiAction[] actions, ImageSettings imageSettings, PictureBoxImageHolder pictureBox)
    {
        ClientSize = new Size(imageSettings.Width, imageSettings.Height);

        var mainMenu = new MenuStrip();
        mainMenu.Items.AddRange(actions.ToMenuItems());
        Controls.Add(mainMenu);

        pictureBox.RecreateImage(imageSettings);
        pictureBox.Dock = DockStyle.Fill;
        Controls.Add(pictureBox);
    }
}