using System.Drawing;

namespace TagsCloud.TextProcessing
{
    public interface IWordsConfig
    {
        public Font FontName { get; set; }
        public Color Color { get; set; }
        public string Path { get; set; }
    }
}
