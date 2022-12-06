namespace TagCloudApp.Domain;

public static class SettingsForm
{
    public static SettingsForm<TSettings> For<TSettings>(TSettings settings) => new(settings);
}

public class SettingsForm<TSettings> : Form
{
    public SettingsForm(TSettings settings)
    {
        var okButton = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Dock = DockStyle.Bottom
        };
        Controls.Add(okButton);
        Controls.Add(
            new PropertyGrid
            {
                SelectedObject = settings,
                Dock = DockStyle.Fill
            }
        );
        AcceptButton = okButton;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Text = "Settings";
    }
}