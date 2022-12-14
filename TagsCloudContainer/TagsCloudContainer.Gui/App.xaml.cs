using System.Windows;
using DryIoc;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Gui;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = GetMainWindow();
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    private static MainWindow GetMainWindow()
    {
        var container = ContainerHelper.RegisterDefaultSingletonContainer();
        container.Register<MainWindow>();
        container.RegisterDelegate<IImageListProvider>(r => r.Resolve<MainWindow>());
        container.RegisterDelegate<ISettingsFactory>(r => r.Resolve<MainWindow>());
        container.Register<IGraphicsProvider, GuiGraphicsProvider>(Reuse.Transient);
        container.RegisterDelegate(r =>
            (GuiGraphicsProviderSettings)r.Resolve<Settings>().GraphicsProviderSettings, Reuse.Transient);
        container
            .Register<ISettingsCreator<CircularLayouterAlgorithmSettings>,
                CircularLayouterAlgorithmSettingsCreator>(Reuse.Transient);
        container.Register<ISettingsCreator<ClassicDrawerSettings>, ClassicDrawerSettingsCreator>(Reuse.Transient);
        container.Register<ISettingsCreator<RandomColoredDrawerSettings>, RandomColoredDrawerSettingsCreator>(
            Reuse.Transient);
        container.Register<ISettingsEditor<GuiGraphicsProviderSettings>, GuiGraphicsProviderSettingsEditor>(
            Reuse.Transient);

        return container.Resolve<MainWindow>();
    }
}