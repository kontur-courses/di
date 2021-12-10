using TagCloud.Templates;

namespace TagCloud
{
    public interface IVisualizer
    {
        void Draw(ITemplate template, string filename);
    }
}