
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using System;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly Lazy<IPainter> kochPainter;

        public KochFractalAction(Lazy<IPainter> kochPainter)
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