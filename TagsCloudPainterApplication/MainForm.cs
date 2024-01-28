using TagsCloudPainterApplication.Actions;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication;

public partial class MainForm : Form
{
    public MainForm(IUiAction[] actions, ImageSettings imageSettings, PictureBoxImageHolder pictureBox)
    {
        ArgumentNullException.ThrowIfNull(actions);
        if (actions.Count() == 0)
            throw new ArgumentException(nameof(actions));
        ArgumentNullException.ThrowIfNull(imageSettings);
        ArgumentNullException.ThrowIfNull(pictureBox);

        ClientSize = new Size(imageSettings.Width, imageSettings.Height);

        var mainMenu = new MenuStrip();
        mainMenu.Items.AddRange(actions.ToMenuItems());
        Controls.Add(mainMenu);

        pictureBox.Dock = DockStyle.Fill;
        Controls.Add(pictureBox);
    }
}