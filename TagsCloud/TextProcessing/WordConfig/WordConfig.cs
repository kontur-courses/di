using System.Drawing;

namespace TagsCloud.TextProcessing.Word
{
    public class WordConfig : IWordsConfig
    {
        public Font FontName { get; set; }
        public Color Color { get; set; }
        public string Path { get; set; }
    }
}
