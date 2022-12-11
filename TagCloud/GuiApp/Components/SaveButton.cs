using GuiApp.Components;

namespace GuiApp;

public class SaveButton : Button
{
    public static event EventHandler? SaveRequired;
    public SaveButton()
    {
        Dock = DockStyle.Top;
        Text = "Save";
    }

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        if (Viewport.Instance.Image is null)
        {
            MessageBox.Show("Image not rendered to save", "Operation is unavailable", MessageBoxButtons.OK);
            return;
        }
        
        var fileDialog = new SaveFileDialog();

        fileDialog.Filter = "PNG image|*.png;";
        fileDialog.FilterIndex = 0;
        fileDialog.RestoreDirectory = true;

        if (fileDialog.ShowDialog() != DialogResult.OK) return;
        Viewport.Instance.Image?.Save(fileDialog.FileName);
    }
}