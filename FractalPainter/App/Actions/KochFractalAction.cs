using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using System;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private Lazy<KochPainter> painter;

        public KochFractalAction(Lazy<KochPainter> painter)
        {
            this.painter = painter;
        }

        public void Perform()
        {
            painter.Value.Paint();
        }

        //public void SetDependency(IImageHolder dependency)
        //{
        //    imageHolder = dependency;
        //}

        //public void SetDependency(Palette dependency)
        //{
        //    palette = dependency;
        //}

        public MenuCategory Category => MenuCategory.Fractals;
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";
    }
}