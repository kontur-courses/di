namespace GuiApp.Controls;

public class ControlWithDescription<T> : TableLayoutPanel where T : Control
{
    public ControlWithDescription(T control, string description)
    {
        Control = control;

        SetUpDescription(description);
        SetUpComponent();

        ColumnCount = 2;
        RowCount = 1;
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        Padding = Padding.Empty;
        Margin = Padding.Empty;
        AutoSize = true;
        Dock = DockStyle.Top;
    }

    public sealed override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    public sealed override DockStyle Dock
    {
        get => base.Dock;
        set => base.Dock = value;
    }

    public T Control { get; }
    public Label Description { get; } = new();

    private void SetUpDescription(string description)
    {
        Description.Text = description;
        Description.ImageAlign = ContentAlignment.MiddleCenter;
        Description.Anchor = AnchorStyles.Left;
        Description.AutoSize = true;
        Description.Anchor = AnchorStyles.None;
        Controls.Add(Description);
    }

    private void SetUpComponent()
    {
        Control.Anchor = AnchorStyles.Right;
        Controls.Add(Control);
    }
}