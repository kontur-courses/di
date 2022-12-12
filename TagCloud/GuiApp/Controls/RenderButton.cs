namespace GuiApp.Controls;

public class RenderButton : Button
{
    public RenderButton()
    {
        Dock = DockStyle.Top;
        Text = "Render";
    }

    public bool IsRenderAvailable { get; set; }
    public static event EventHandler? RenderRequired;

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        if (!IsRenderAvailable)
        {
            MessageBox.Show("File with words is not selected", "Operation is unavailable", MessageBoxButtons.OK);
            return;
        }

        RenderRequired?.Invoke(this, e);
    }
}