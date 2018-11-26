namespace FractalPainting.Solved.Step09.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    }
}