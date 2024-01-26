using TagsCloudPainter.Settings;
using TagsCloudPainterApplication.Actions;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication;

public partial class MainForm : Form
{
    public MainForm(IUiAction[] actions, ImageSettings imageSettings, PictureBoxImageHolder pictureBox)
    {
        if(actions is null || actions.Length == 0 || imageSettings is null || pictureBox is null)
            throw new ArgumentNullException("MainForm cann't be injected with nullable reference");

        ClientSize = new Size(imageSettings.Width, imageSettings.Height);

        var mainMenu = new MenuStrip();
        mainMenu.Items.AddRange(actions.ToMenuItems());
        Controls.Add(mainMenu);

        pictureBox.Dock = DockStyle.Fill;
        Controls.Add(pictureBox);
    }
}