using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly KochPainter painter;

        public KochFractalAction(KochPainter painter)
        {
            this.painter = painter;
        }

        #region IUiAction

        public string Category => "Фракталы";
        public int Order => 1;
        public int CategoryOrder => 1;
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform() => painter.Paint();

        #endregion
    }
}