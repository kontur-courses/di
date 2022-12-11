namespace GuiApp.Components;

public class ColorProperties : ControlWithDescription<Button>
{
    public event EventHandler? ColorChanged;
    
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

    public ColorProperties(string description) : base(new Button(), description)
    {
        Color = colorDialog.Color;
        colorDialog.FullOpen = true;
        Control.Click += OnClick;
    }

    private void OnClick(object? sender, EventArgs eventArgs)
    {
        colorDialog.ShowDialog();
        Color = colorDialog.Color;
    }

    private ColorDialog colorDialog = new ColorDialog();
}