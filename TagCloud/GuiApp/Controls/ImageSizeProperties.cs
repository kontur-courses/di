namespace GuiApp.Controls;

public class ImageSizeProperties : TableLayoutPanel
{
    public event EventHandler? ValueChanged;
    
    public ImageSizeProperties(Size size)
    {
        SetUpNumeric(width.Control, size.Width);
        SetUpNumeric(height.Control, size.Height);
        
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        Controls.Add(width);
        Controls.Add(height);
        
        Padding = Padding.Empty;
        Margin = Padding.Empty;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        Dock = DockStyle.Top;
    }

    public int Width
    {
        get => (int)width.Control.Value;
        set => width.Control.Value = value;
    }
    
    public int Height
    {
        get => (int)height.Control.Value;
        set => height.Control.Value = value;
    }
    
    private ControlWithDescription<NumericUpDown> width = new (new NumericUpDown(), "Width");
    private ControlWithDescription<NumericUpDown> height = new (new NumericUpDown(), "Height");

    private void SetUpNumeric(NumericUpDown numeric, int value)
    {
        numeric.Maximum = int.MaxValue;
        numeric.Minimum = 0;
        numeric.Anchor = AnchorStyles.Right;
        numeric.Value = value;
        numeric.ValueChanged += (sender, args) => ValueChanged?.Invoke(sender, args);
    }
}