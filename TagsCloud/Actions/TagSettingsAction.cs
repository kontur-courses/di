using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;
using TagsCloud.Settings;

namespace TagsCloud.Actions;

public class TagSettingsAction : IUiAction
{
    private readonly TagSettings tag;

    public TagSettingsAction(TagSettings tag)
    {
        this.tag = tag;
    }
    public MenuCategory Category => MenuCategory.Settings;
    public string Name => "Облако тегов...";
    public string Description => "";
    public void Perform()
    {
        SettingsForm.For(tag).ShowDialog();
    }
}