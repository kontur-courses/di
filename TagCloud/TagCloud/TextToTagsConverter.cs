using WeCantSpell.Hunspell;
using System.Text.RegularExpressions;

namespace TagCloud;

public class TextToTagsConverter : ITextToTagsConverter
{
    public ITextLoader TextLoader { get; set; }

    private WordList analysator;

    public TextToTagsConverter(ITextLoader textLoader)
    {
        TextLoader = textLoader;
        analysator = WordList.CreateFromFiles(@"Languages/ru_RU/ru_RU.dic", @"Languages/ru_RU/ru_RU.aff");
    }

    public Dictionary<string, int> GetTags()
    {
        var text = TextLoader.Load().ToLower();
        var words = Regex.Split(text, @"\W+").ToList();

        return ComputeWordsFrequency(words);
    }

    public static Dictionary<string, int> ComputeWordsFrequency(List<string> text)
    {
        var dictionary = new Dictionary<string, int>();
        foreach (var word in text)
        {
            if (!dictionary.ContainsKey(word))
                dictionary.Add(word, 1);
            else
                dictionary[word]++;
        }

        return dictionary;
    }
}