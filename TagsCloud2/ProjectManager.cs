using TagsCloud2.InputManager;
using TagsCloud2.TagsCloudMakerManager;

namespace TagsCloud2;

public class ProjectManager : IProjectManager
{
    private IInputManager dataCollector;
    private ITagsCloudMakerManager tagsCloudMakerManager;

    public ProjectManager(IInputManager dataCollector,
        ITagsCloudMakerManager tagsCloudMakerManager)
    {
        this.dataCollector = dataCollector;
        this.tagsCloudMakerManager = tagsCloudMakerManager;
    }


    public void CollectInformationAndMakePicture()
    {
        dataCollector.GatherInformation();
        tagsCloudMakerManager.MakeTagsCloud(
            dataCollector.Path(),
            dataCollector.FontFamilyName(),
            dataCollector.BrushColor(),
            dataCollector.PathToSave(),
            dataCollector.FormatToSave(),
            dataCollector.IsVerticalWords(),
            dataCollector.Size()
        );
    }
}
