using TagsCloudContainer;

namespace TagsCloudVisualization;

public interface IDrawingModel
{
    public DrawingModel GetDrawingModel(CommandLineOptions options);
}