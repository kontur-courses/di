using System.Windows;

namespace TagsCloudContainer.Gui;

public partial class ClassicDrawerSettingsCreator : ISettingsCreator<ClassicDrawerSettings>
{
    public ClassicDrawerSettingsCreator()
    {
        InitializeComponent();
    }

    public ClassicDrawerSettings DrawerSettings { get; } = new();

    ClassicDrawerSettings? ISettingsCreator<ClassicDrawerSettings>.ShowCreate()
    {
        var result = ShowDialog() ?? false;
        return result ? DrawerSettings : null;
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