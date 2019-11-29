using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.UiActions;
using System;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private Lazy<KochPainter> kochPainter;

        public KochFractalAction(Lazy<KochPainter> kochPainter)
        {
            this.kochPainter = kochPainter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            kochPainter.Value.Paint();
        }
    }
}