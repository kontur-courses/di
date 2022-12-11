namespace GuiApp.Controls;

public class FontComboBox : ComboBox
{
    public FontComboBox()
    {
        var itemsCount = FontFamily.Families.Length;
        var fonts = new object[itemsCount];
        for (var i = 0; i < itemsCount; i++)
            fonts[i] = FontFamily.Families[i].Name;

        Items.AddRange(fonts);
        SelectedItem = FontFamily.GenericSansSerif.Name;
    }
}