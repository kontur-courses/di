using System.Drawing;

namespace TagsCloud.TextProcessing.WordConfig
{
    public interface IWordsConfig
    {
        Font FontName { get; set; }
        Color Color { get; set; }
        string Path { get; set; }
        string[] FilerNames { get; set; }
        string[] ConvertersNames { get; set; }
        string LayoutName { get; set; }
        string TagGeneratorName { get; set; }
    }
}
