using FractalPainting.App.Fractals;

namespace FractalPainting.App.Actions
{
    public interface IDragonPainterFactory
    {
        DragonPainter Creatre(DragonSettings settings);
    }
}