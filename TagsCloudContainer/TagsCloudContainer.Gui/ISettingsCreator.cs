namespace TagsCloudContainer.Gui;

public interface ISettingsCreator<TSetting>
{
    TSetting? ShowCreate();
}