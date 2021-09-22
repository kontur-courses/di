using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
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