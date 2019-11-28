using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using System;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private KochPainter painter;

        public KochFractalAction(KochPainter painter)
        {
            this.painter = painter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            painter.Paint();
        }
    }
}