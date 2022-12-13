using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Gui;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : IImageListProvider, ISettingsFactory
{
    private readonly Func<ISettingsCreator<CircularLayouterAlgorithmSettings>>
        circularLayouterSettingsEditorFactory;

    private readonly Func<ISettingsCreator<ClassicDrawerSettings>> classicDrawerSettingsEditorFactory;

    private readonly IFunnyWordsSelector funnyWordsSelector;

    private readonly Func<ISettingsEditor<GuiGraphicsProviderSettings>> guiGraphicsProviderSettingsEditorFactory;
    private readonly Func<ISettingsCreator<RandomColoredDrawerSettings>> randomColoredDrawerSettingsCreator;

    private readonly Func<MultiDrawer> multiDrawerFactory;

    private readonly Timer timer;

    public MainWindow(
        Func<ISettingsCreator<CircularLayouterAlgorithmSettings>> circularLayouterSettingsEditorFactory,
        Func<ISettingsCreator<ClassicDrawerSettings>> classicDrawerSettingsEditorFactory,
        Func<ISettingsEditor<GuiGraphicsProviderSettings>> guiGraphicsProviderSettingsEditorFactory,
        Func<ISettingsCreator<RandomColoredDrawerSettings>> randomColoredDrawerSettingsCreator,
        Func<MultiDrawer> multiDrawerFactory,
        IFunnyWordsSelector funnyWordsSelector)
    {
        this.circularLayouterSettingsEditorFactory = circularLayouterSettingsEditorFactory;
        this.classicDrawerSettingsEditorFactory = classicDrawerSettingsEditorFactory;
        this.guiGraphicsProviderSettingsEditorFactory = guiGraphicsProviderSettingsEditorFactory;
        this.randomColoredDrawerSettingsCreator = randomColoredDrawerSettingsCreator;
        this.multiDrawerFactory = multiDrawerFactory;
        this.funnyWordsSelector = funnyWordsSelector;
        InitializeComponent();
        DataContext = this;
        LayouterAlgorithmSettingsList.CollectionChanged += (_, _) => StartingThrottlingOnWork();
        DrawerSettingsList.CollectionChanged += (_, _) => StartingThrottlingOnWork();
        timer = new(StartDrawing);
    }

    public ObservableCollection<DrawerSettings> DrawerSettingsList { get; } = new()
        { new ClassicDrawerSettings() };

    public ObservableCollection<LayouterAlgorithmSettings> LayouterAlgorithmSettingsList { get; } = new()
        { new CircularLayouterAlgorithmSettings() };

    public GuiGraphicsProviderSettings GraphicsSettings { get; set; } = new();

    public ObservableCollection<byte[]> ImageBytes { get; set; } = new();

    public bool AutoDraw { get; set; }

    public LayouterAlgorithmSettings? SelectedLayouterAlgorithmSettings { get; set; }

    public DrawerSettings? SelectedDrawerSettings { get; set; }

    public void AddImageBits(byte[] imageBytes)
    {
        ImageBytes.Add(imageBytes);
        // ImagesListBox.ItemsSource = ImageBytes;
    }

    public Settings Build()
    {
        return new()
        {
            GraphicsProviderSettings = GraphicsSettings,
            LayouterAlgorithmSettings = LayouterAlgorithmSettingsList.ToList(),
            DrawerSettings = DrawerSettingsList.ToList()
        };
    }

    private void StartingThrottlingOnWork()
    {
        if (!AutoDraw) return;
        ImageBytes.Clear();
        timer.Change(TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
    }

    private void WordsChanged(object sender, RoutedEventArgs e)
    {
        StartingThrottlingOnWork();
    }

    private void RemoveSelectedDrawerSettings(object sender, RoutedEventArgs e)
    {
        if (SelectedDrawerSettings is not null)
            DrawerSettingsList.Remove(SelectedDrawerSettings);
        StartingThrottlingOnWork();
    }

    private void NewClassicDrawerSettings(object sender, RoutedEventArgs e)
    {
        var editor = classicDrawerSettingsEditorFactory();
        CallCreator(editor, DrawerSettingsList);
    }

    private void RemoveSelectedLayouterAlgorithmSettings(object sender, RoutedEventArgs e)
    {
        if (SelectedLayouterAlgorithmSettings is not null)
            LayouterAlgorithmSettingsList.Remove(SelectedLayouterAlgorithmSettings);
        StartingThrottlingOnWork();
    }

    private void NewCircularLayouterAlgorithmSettings(object sender, RoutedEventArgs e)
    {
        var editor = circularLayouterSettingsEditorFactory();
        CallCreator(editor, LayouterAlgorithmSettingsList);
    }

    private void AutoDrawIsChanged(object sender, RoutedEventArgs e)
    {
        AutoDraw = ((CheckBox)sender).IsChecked ?? false;
    }

    private void StartDrawing(object sender, RoutedEventArgs e)
    {
        ImageBytes.Clear();
        StartDrawingUsingDispatcher();
    }

    private void StartDrawing(object? state)
    {
        StartDrawingUsingDispatcher();
    }

    private async void StartDrawingUsingDispatcher()
    {
        await Dispatcher.InvokeAsync(() =>
        {
            IsEnabled = false;
            var allWords = Words.Text.ReplaceLineEndings("\n").Split('\n',
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var funnyCloudWords = funnyWordsSelector.RecognizeFunnyCloudWords(
                allWords);
            var multiDrawer = multiDrawerFactory();
            multiDrawer.Draw(funnyCloudWords);
            IsEnabled = true;
        });
    }

    private void EditGraphicsProviderSettings(object sender, RoutedEventArgs e)
    {
        var editor = guiGraphicsProviderSettingsEditorFactory();
        Hide();
        GraphicsSettings = editor.ShowEdit(GraphicsSettings);
        Show();
        StartingThrottlingOnWork();
    }

    private void NewRandomColoredDrawerSettings(object sender, RoutedEventArgs e)
    {
        var editor = randomColoredDrawerSettingsCreator();
        CallCreator(editor, DrawerSettingsList);
    }

    private void CallCreator<T, TBase>(ISettingsCreator<T> editor, ICollection<TBase> collection) where T : TBase
    {
        Hide();
        var settings = editor.ShowCreate();
        if (settings is null) return;
        collection.Add(settings);
        Show();
        StartingThrottlingOnWork();
    }
}