using GuiApp.Controls;
using TagCloud;

namespace GuiApp;

public sealed class TagCloudForm : Form
{
    private readonly Ui ui;

    public TagCloudForm(ApplicationProperties appProperties)
    {
        Size = new Size(960, 540);
        MinimumSize = Size;
        ApplicationProperties = appProperties;
        ui = new Ui(ApplicationProperties) { Dock = DockStyle.Fill };
        Controls.Add(ui);
        Show();
    }

    private ApplicationProperties ApplicationProperties { get; }
}