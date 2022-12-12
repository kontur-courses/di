namespace GuiApp.Controls;

public class Viewport : PictureBox
{
    private Viewport()
    {
        BorderStyle = BorderStyle.FixedSingle;
        SizeMode = PictureBoxSizeMode.Zoom;
    }

    public static Viewport Instance { get; } = new() { Dock = DockStyle.Fill };
}