using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App
{
    public interface IDragonPainterFactory
    {
        DragonPainter CreatePainter(IImageHolder imageHolder, DragonSettings settings);
    }
}