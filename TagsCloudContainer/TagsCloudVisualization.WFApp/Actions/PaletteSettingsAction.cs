using TagsCloudVisualization.Common;
using TagsCloudVisualization.WFApp.Common;
using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp.Actions;

public class PaletteSettingsAction : IUiAction
{
    private readonly Palette palette;

    public PaletteSettingsAction(Palette palette)
    {
        this.palette = palette;
    }

    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Палитра...";
    public string Description => "Цвета для рисования тегов";

    public void Perform()
    {
        SettingsForm.For(palette).ShowDialog();
    }
}