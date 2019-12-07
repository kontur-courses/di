using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step01.App.Fractals;
using FractalPainting.Solved.Step01.Infrastructure.Injection;
using FractalPainting.Solved.Step01.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.Solved.Step01.App.Actions
{
    public class KochFractalAction : IUiAction, INeed<IImageHolder>, INeed<Palette>
    {
        private IImageHolder imageHolder;
        private Palette palette;

        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }

        public void SetDependency(Palette dependency)
        {
            palette = dependency;
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