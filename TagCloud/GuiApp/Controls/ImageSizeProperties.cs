namespace GuiApp.Controls;

public sealed class ImageSizeProperties : TableLayoutPanel
{
    private readonly ControlWithDescription<NumericUpDown> height = new(new NumericUpDown(), "Height");

    private readonly ControlWithDescription<NumericUpDown> width = new(new NumericUpDown(), "Width");

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

    public int ImageWidth
    {
        get => (int)width.Control.Value;
        set => width.Control.Value = value;
    }

    public int ImageHeight
    {
        get => (int)height.Control.Value;
        set => height.Control.Value = value;
    }

    public event EventHandler? ValueChanged;

    private void SetUpNumeric(NumericUpDown numeric, int value)
    {
        numeric.Maximum = int.MaxValue;
        numeric.Minimum = 0;
        numeric.Anchor = AnchorStyles.Right;
        numeric.Value = value;
        numeric.ValueChanged += (sender, args) => ValueChanged?.Invoke(sender, args);
    }
}