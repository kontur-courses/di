using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App.Actions;

public interface IDragonPainterFactory
{
    DragonPainter CreateDragonPainter(DragonSettings settings);
}