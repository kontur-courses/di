using FractalPainting.Solved.Step11.App.Fractals;
using FractalPainting.Solved.Step11.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step11.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly KochPainter kochPainter;

        public KochFractalAction(KochPainter kochPainter)
        {
            this.kochPainter = kochPainter;
        }

        public MenuCategory Category => MenuCategory.Fractals;
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            kochPainter.Paint();
        }
    }
}