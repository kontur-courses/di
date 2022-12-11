namespace GuiApp.Components;

public class Viewport : PictureBox
{
    private Viewport()
    {
        Dock = DockStyle.Fill;
        BorderStyle = BorderStyle.FixedSingle;
        SizeMode = PictureBoxSizeMode.Zoom;
    }

    public static Viewport Instance { get; } = new();
}