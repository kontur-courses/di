using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App.Fractals
{
    public interface IDragonPainterFactory
    {
        public DragonPainter CreateDragonPainter(IImageHolder imageHolder, DragonSettings settings);
    }
}
