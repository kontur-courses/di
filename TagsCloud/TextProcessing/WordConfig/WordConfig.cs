using System.Drawing;

namespace TagsCloud.TextProcessing.WordConfig
{
    public class WordConfig : IWordsConfig
    {
        public Font FontName { get; set; }
        public Color Color { get; set; }
        public string Path { get; set; }
        public string[] FilerNames { get; set; }
        public string[] ConvertersNames { get; set; }
    }
}
