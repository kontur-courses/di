using System.Windows;
using DeepMorphy;
using DryIoc;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Gui;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        var container = new Container();
        container.Register<MorphAnalyzer>(Reuse.Singleton);
        container.Register<IFunnyWordsSelector, DeepMorphyFunnyWordsSelector>(Reuse.Singleton);
        container.Register<MultiDrawer>(Reuse.Transient);
        container.Register<MainWindow>(Reuse.Singleton);
        container.Register<IGraphicsProvider, GuiGraphicsProvider>(Reuse.Transient);
        container.RegisterDelegate<IImageListProvider>(r => r.Resolve<MainWindow>(), Reuse.Singleton);
        container.RegisterDelegate<ISettingsFactory>(r => r.Resolve<MainWindow>(), Reuse.Singleton);
        container.RegisterDelegate(r =>
            (GuiGraphicsProviderSettings)r.Resolve<Settings>().GraphicsProviderSettings, Reuse.Transient);
        container
            .Register<ISettingsCreator<CircularLayouterAlgorithmSettings>,
                CircularLayouterAlgorithmSettingsCreator>();
        container.Register<ISettingsCreator<ClassicDrawerSettings>, ClassicDrawerSettingsCreator>();
        container.Register<ISettingsEditor<GuiGraphicsProviderSettings>, GuiGraphicsProviderSettingsEditor>();
        container.Register<IDrawerFactory, ClassicDrawerFactory>(Reuse.Singleton);
        container.Register<ILayouterAlgorithmFactory, CircularCloudLayouterFactory>(Reuse.Singleton);
        container.RegisterDelegate(r => r.Resolve<ISettingsFactory>().Build(),
            Reuse.Transient);

        var mainWindow = container.Resolve<MainWindow>();
        MainWindow = mainWindow;
        mainWindow.Show();
    }
}