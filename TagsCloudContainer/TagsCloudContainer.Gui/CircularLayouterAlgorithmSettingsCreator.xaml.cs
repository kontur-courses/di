using System.Windows;

namespace TagsCloudContainer.Gui;

public partial class CircularLayouterAlgorithmSettingsCreator : ISettingsCreator<CircularLayouterAlgorithmSettings>
{
    public CircularLayouterAlgorithmSettingsCreator()
    {
        InitializeComponent();
    }

    public CircularLayouterAlgorithmSettings LayouterAlgorithmSettings { get; } = new();

    CircularLayouterAlgorithmSettings? ISettingsCreator<CircularLayouterAlgorithmSettings>.ShowCreate()
    {
        var result = ShowDialog() ?? false;
        return result ? LayouterAlgorithmSettings : null;
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