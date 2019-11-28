using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    class MyAction : IUiAction
    {
        public string Category => "My Category";

        public string Name => "My Action";

        public string Description => "Test";

        public void Perform()
        {
            throw new System.NotImplementedException();
        }
    }
}
