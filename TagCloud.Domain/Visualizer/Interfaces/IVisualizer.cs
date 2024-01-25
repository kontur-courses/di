namespace TagCloud.Domain.Visualizer.Interfaces;

public interface IVisualizer
{
    public Image Visualize(IEnumerable<string> words);
}