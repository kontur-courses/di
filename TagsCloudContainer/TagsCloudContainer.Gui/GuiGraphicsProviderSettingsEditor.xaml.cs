using System.Windows;

namespace TagsCloudContainer.Gui;

public partial class GuiGraphicsProviderSettingsEditor : ISettingsEditor<GuiGraphicsProviderSettings>
{
    public GuiGraphicsProviderSettingsEditor()
    {
        InitializeComponent();
    }

    public GuiGraphicsProviderSettings GraphicsProviderSettings { get; private set; } = new();

    GuiGraphicsProviderSettings ISettingsEditor<GuiGraphicsProviderSettings>.ShowEdit(
        GuiGraphicsProviderSettings settings)
    {
        GraphicsProviderSettings = new()
        {
            Height = settings.Height,
            Width = settings.Width,
            Save = settings.Save,
            SavePath = settings.SavePath
        };
        var result = ShowDialog() ?? false;
        return result ? GraphicsProviderSettings : settings;
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void Submit(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }
}