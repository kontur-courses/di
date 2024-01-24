using TagsCloudVisualization.Common;
using TagsCloudVisualization.WFApp.Common;
using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp.Actions;

public class TagsSettingsAction : IUiAction
{
    private readonly TagsSettings tagsSettings;

    public TagsSettingsAction(TagsSettings tagsSettings)
    {
        this.tagsSettings = tagsSettings;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Палитра и шрифт...";
    public string Description => "Цвета и шрифт для рисования тегов";

    public void Perform()
    {
        SettingsForm.For(tagsSettings).ShowDialog();
    }
}