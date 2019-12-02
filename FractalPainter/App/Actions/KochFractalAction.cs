using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private KochPainter painter { get; }
        public KochFractalAction(KochPainter painter)
        {
            this.painter = painter;
        }
        //public void SetDependency(IImageHolder dependency)
        //{
        //    imageHolder = dependency;
        //}

        //public void SetDependency(Palette dependency)
        //{
        //    palette = dependency;
        //}

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            painter.Paint();
        }
    }
}