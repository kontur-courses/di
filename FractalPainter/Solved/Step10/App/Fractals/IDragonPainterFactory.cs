namespace FractalPainting.Solved.Step10.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    }
}