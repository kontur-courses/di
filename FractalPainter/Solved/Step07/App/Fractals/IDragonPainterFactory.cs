namespace FractalPainting.Solved.Step07.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    }
}