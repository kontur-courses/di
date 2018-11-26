using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step03.App.Fractals;
using FractalPainting.Solved.Step03.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.Solved.Step03.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;

        public KochFractalAction(IImageHolder imageHolder, Palette palette)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            var container = new StandardKernel();
            container.Bind<IImageHolder>().ToConstant(imageHolder);
            container.Bind<Palette>().ToConstant(palette);

            container.Get<KochPainter>().Paint();
        }
    }
}