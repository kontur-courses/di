namespace FractalPainting.Infrastructure.UiActions
{
    public interface IUiAction
    {
        string Category { get; }
        int CategoryOrder { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}