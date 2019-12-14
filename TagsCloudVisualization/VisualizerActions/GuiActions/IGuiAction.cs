using TagsCloudVisualization.GUI;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public interface IGuiAction : IVisualizerAction
    {
        MenuCategory GetMenuCategory();
    }
}