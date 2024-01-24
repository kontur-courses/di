using TagsCloudVisualization.Common;
using TagsCloudVisualization.WFApp.Common;
using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp;

public class MainForm : Form
{
    public MainForm(IUiAction[] actions,
        PictureBoxImageHolder pictureBox,
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
        Text = "Tags Cloud Visualizator";
    }
}