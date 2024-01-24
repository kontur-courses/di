using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.WFApp.Common;
using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp.Actions;

public class SourceSettingsAction : IUiAction
{
    private readonly SourceSettings sourceSettings;

    public SourceSettingsAction(SourceSettings sourceSettings)
    {
        this.sourceSettings = sourceSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Источник...";
    public string Description => "Изменить источник текста";

    public void Perform()
    {
        var dialog = new OpenFileDialog()
        {
            CheckFileExists = false,
            InitialDirectory = Path.GetFullPath("/"),
            DefaultExt = "txt",
            Filter = "Текстовый файл (*.txt)|*.txt" 
        };
        var res = dialog.ShowDialog();
        
        if (res == DialogResult.OK)
            sourceSettings.Path = dialog.FileName;
    }
}