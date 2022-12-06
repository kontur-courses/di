using TagCloudApp.Domain;

namespace TagCloudApp;

public partial class MainForm : Form
{
    public MainForm(
        IEnumerable<IUiAction> actions,
        PictureBoxImageHolder pictureBox,
        ImageSettings imageSettings
    )
    {
        ClientSize = new Size(imageSettings.Width, imageSettings.Height);
        imageSettings.OnChange += size => ClientSize = size;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

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
        Text = "Tag cloud";
    }
}