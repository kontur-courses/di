namespace FractalPainting.Solved.Step10.Infrastructure.UiActions
{
    public interface IUiAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}