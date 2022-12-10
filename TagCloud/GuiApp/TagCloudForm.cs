using TagCloud;

namespace GuiApp;

public class TagCloudForm : Form
{
    private readonly System.ComponentModel.IContainer? components = null;
    private ApplicationProperties ApplicationProperties { get; }
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
    
    public TagCloudForm(ApplicationProperties appProperties)
    {
        Size = new Size(1280, 720);
        ApplicationProperties = appProperties;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        viewport.Dock = DockStyle.Fill;
        components?.Add(viewport);
    }

    private readonly PictureBox viewport = new();
}