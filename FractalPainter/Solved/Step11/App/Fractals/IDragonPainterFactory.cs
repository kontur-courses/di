namespace FractalPainting.Solved.Step11.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    }
}