namespace TagCloud.Interfaces.GUI.UIActions
{
    public interface IUiAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}