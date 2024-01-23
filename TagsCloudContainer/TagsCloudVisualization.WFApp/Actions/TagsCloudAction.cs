using TagsCloudVisualization.WFApp.Infrastructure;

namespace TagsCloudVisualization.WFApp.Actions;

public class TagsCloudAction : IUiAction
{
    private readonly TagsCloudVisualizator tagsCloudVisualizator;

    public TagsCloudAction(TagsCloudVisualizator tagsCloudVisualizator)
    {
        this.tagsCloudVisualizator = tagsCloudVisualizator;
    }

    public MenuCategory Category => MenuCategory.TagsClouds;
    public string Name => "Облако тегов";
    public string Description => "Облако тегов";

    public void Perform()
    {
        tagsCloudVisualizator.DrawTagsCloud();
    }
}