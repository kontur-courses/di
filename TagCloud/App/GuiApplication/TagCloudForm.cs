namespace App.GuiApplication;

public class TagCloudForm : Form
{
    private readonly System.ComponentModel.IContainer? components = null;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
    
    public TagCloudForm()
    {
        Size = new Size(1280, 720);
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        viewport.Dock = DockStyle.Fill;
        components?.Add(viewport);
    }

    private readonly PictureBox viewport = new();
}