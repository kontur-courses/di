namespace GuiApp.Controls;

public class RenderButton : Button
{
    public static event EventHandler? RenderRequired;
    public bool IsRenderAvailable { get; set; }

    public RenderButton()
    {
        Dock = DockStyle.Top;
        Text = "Render";
    }

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