namespace FractalPainting.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    }
}