
namespace TagsCloudVisualization.Styling
{
    public class FontProperties
    {
        public string Name { get; }
        public int MinSize { get; }
        
        public FontProperties(string name, int minSize)
        {
            Name = name;
            MinSize = minSize;
        }
    }
}