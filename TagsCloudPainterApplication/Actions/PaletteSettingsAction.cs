using TagsCloudPainterApplication.Infrastructure;
using TagsCloudPainterApplication.Infrastructure.Settings;

namespace TagsCloudPainterApplication.Actions;

public class PaletteSettingsAction : IUiAction
{
    private readonly Palette palette;

    public PaletteSettingsAction(Palette palette)
    {
        this.palette = palette ?? throw new ArgumentNullException(nameof(palette));
    }

    public string Category => "Настройки";
    public string Name => "Палитра...";
    public string Description => "Цвета для рисования облака тэгов";

    public void Perform()
    {
        SettingsForm.For(palette).ShowDialog();
    }
}