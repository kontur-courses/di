using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly Lazy<KochPainter> _kochPainter;
        public KochFractalAction(Lazy<KochPainter> kochPainter)
        {
            _kochPainter = kochPainter;
        }
        
        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            _kochPainter.Value.Paint();
        }
    }
}