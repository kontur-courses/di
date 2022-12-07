using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp;

public partial class MainForm : Form
{
    public MainForm(
        IEnumerable<IUiAction> actions,
        PictureBoxImageHolder pictureBox,
        IImageSettingsProvider imageSettingsProvider
    )
    {
        var imageSettings = imageSettingsProvider.GetImageSettings();
        ClientSize = new Size(imageSettings.Width, imageSettings.Height);
        imageSettings.OnChange += size => ClientSize = size;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var mainMenu = new MenuStrip();
        mainMenu.Items.AddRange(actions.ToMenuItems());
        Controls.Add(mainMenu);

        pictureBox.RecreateImage();
        pictureBox.Dock = DockStyle.Fill;
        Controls.Add(pictureBox);
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Text = "Tag cloud";
    }
}