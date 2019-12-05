using TagsCloudVisualization.VisualizerActions;

namespace TagsCloudVisualization.GUI.GuiActions
{
    public interface IGuiAction : IVisualizerAction
    {
        MenuCategory GetMenuCategory();
    }
}