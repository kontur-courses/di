using FractalPainting.App.Fractals;

namespace FractalPainting
{
    public interface IDragonFactory
    {
        DragonPainter CreateDragonPainter(DragonSettings settings);
    }
}