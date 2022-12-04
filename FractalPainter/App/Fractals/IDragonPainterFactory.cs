using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(IImageHolder imageHolder, DragonSettings settings);
    }
}