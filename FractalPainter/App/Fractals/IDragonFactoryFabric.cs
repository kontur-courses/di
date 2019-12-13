namespace FractalPainting.App.Fractals
{
    public interface IDragonFactoryFabric
    {
        DragonPainter Create(DragonSettings settings);
    }
}