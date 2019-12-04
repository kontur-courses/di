namespace TagCloud.Interfaces.GUI.UIActions
{
    public interface IUIAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}