using System.Windows;

namespace TagsCloudContainer.Gui;

public partial class RandomColoredDrawerSettingsCreator : ISettingsCreator<RandomColoredDrawerSettings>
{
    public RandomColoredDrawerSettingsCreator()
    {
        InitializeComponent();
    }

    public RandomColoredDrawerSettings DrawerSettings { get; } = new();
    
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

    public RandomColoredDrawerSettings? ShowCreate()
    {
        var result = ShowDialog() ?? false;
        return result ? DrawerSettings : null;
    }
}