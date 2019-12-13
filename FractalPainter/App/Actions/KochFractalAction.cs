using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction //, INeed<IImageHolder>, INeed<Palette>
    {
        //private IImageHolder imageHolder;
        //private Palette palette;
        private KochPainter kochPainter;


        //public KochFractalAction(IImageHolder _imageHolder, Palette _palette)
        public KochFractalAction(KochPainter _kochPainter)
        {
            kochPainter = _kochPainter;
            //imageHolder = _imageHolder;
            //palette = _palette;
        }
        /*
        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }

        public void SetDependency(Palette dependency)
        {
            palette = dependency;
        }
        */


        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            //var container = new StandardKernel();
            //container.Bind<IImageHolder>().ToConstant(imageHolder);
            //container.Bind<Palette>().ToConstant(palette);
            //container.Bind<KochPainter>().ToConstant(kochPainter);
            kochPainter.Paint();
            //container.Get<KochPainter>().Paint();
        }
    }
}