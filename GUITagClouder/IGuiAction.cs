namespace GUITagClouder
{
    public interface IGuiAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}