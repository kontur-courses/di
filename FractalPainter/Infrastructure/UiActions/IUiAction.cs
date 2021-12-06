using FractalPainting.App.Actions;

namespace FractalPainting.Infrastructure.UiActions
{
    public interface IUiAction
    {
        string CategoryName { get; }
        Category Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}