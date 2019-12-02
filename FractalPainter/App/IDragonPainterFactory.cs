using FractalPainting.App.Fractals;

namespace FractalPainting.App
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    }
}