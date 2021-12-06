using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        public KochFractalAction(Lazy<KochPainter> painter)
        {
            this.painter = painter;
        }

        private Lazy<KochPainter> painter;


        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            painter.Value.Paint();
        }
    }
}