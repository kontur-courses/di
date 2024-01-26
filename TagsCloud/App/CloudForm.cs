using System.Drawing;
using System.Windows.Forms;
using TagsCloud.App.Settings;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud;

public partial class CloudForm : Form
{
    public CloudForm(IUiAction[] actions,
        PictureBoxImageHolder pictureBox,
        ImageSettings imageSettings)
    {
        InitializeComponent();
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
        Text = "TagCloud Painter";
    }
}