using TagCloud;

namespace GuiApp.Controls;

public class Ui : TableLayoutPanel
{
    private readonly PropertyPanel propertyPanel;

    public Ui(ApplicationProperties properties, IWordsParser wordParser)
    {
        propertyPanel = new PropertyPanel(properties, wordParser);
        RowCount = 1;
        ColumnCount = 2;
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        Controls.Add(propertyPanel);
        Controls.Add(Viewport.Instance);
    }
}