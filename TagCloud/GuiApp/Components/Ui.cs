using TagCloud;

namespace GuiApp.Components;

public class Ui : TableLayoutPanel
{
    public Ui(ApplicationProperties properties)
    {
        propertyPanel = new PropertyPanel(properties);
        Dock = DockStyle.Fill;
        RowCount = 1;
        ColumnCount = 2;
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        
        Controls.Add(propertyPanel);
        Controls.Add(Viewport.Instance);
    }
    
    private readonly PropertyPanel propertyPanel;
}