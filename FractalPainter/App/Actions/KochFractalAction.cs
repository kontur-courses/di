using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private Lazy<KochPainter> kochPainter;
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;

        public KochFractalAction(IImageHolder imageHolder, Palette palette)
        {
            this.palette = palette;
            this.imageHolder = imageHolder;
            kochPainter = new Lazy<KochPainter>(() => new KochPainter(this.imageHolder, this.palette));
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