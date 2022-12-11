using GuiApp.Controls;
using TagCloud;

namespace GuiApp;

public class TagCloudForm : Form
{
    private ApplicationProperties ApplicationProperties { get; }
    
    public TagCloudForm(ApplicationProperties appProperties)
    {
        Size = new Size(960, 540);
        MinimumSize = Size;
        ApplicationProperties = appProperties;
        ui = new Ui(ApplicationProperties);
        Controls.Add(ui);
        Show();
    }

    private Ui ui;
}