namespace GuiApp.Controls;

public class ColorProperties : ControlWithDescription<Button>
{
    private readonly ColorDialog colorDialog = new();

    public ColorProperties(string description) : base(new Button(), description)
    {
        Color = colorDialog.Color;
        colorDialog.FullOpen = true;
        Control.Click += OnClick;
    }

    public Color Color
    {
        get => colorDialog.Color;
        set
        {
            colorDialog.Color = value;
            ColorChanged?.Invoke(this, EventArgs.Empty);
            Control.BackColor = colorDialog.Color;
        }
    }

    public event EventHandler? ColorChanged;

    private void OnClick(object? sender, EventArgs eventArgs)
    {
        colorDialog.ShowDialog();
        Color = colorDialog.Color;
    }
}