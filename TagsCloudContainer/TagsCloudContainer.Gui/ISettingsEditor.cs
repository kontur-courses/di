namespace TagsCloudContainer.Gui;

public interface ISettingsEditor<T>
{
    GuiGraphicsProviderSettings ShowEdit(GuiGraphicsProviderSettings settings);
}