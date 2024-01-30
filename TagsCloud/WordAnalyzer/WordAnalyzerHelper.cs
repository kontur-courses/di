using TagsCloud.WordAnalyzer;

namespace TagsCloud;

public static class WordAnalyzerHelper
{
    private static readonly Dictionary<PartSpeech, string> converter = new()
    {
        { PartSpeech.Noun, "сущ" },
        { PartSpeech.Pronoun, "мест" },
        { PartSpeech.Verb, "гл" },
        { PartSpeech.Adjective, "прил" },
        { PartSpeech.Conjunction, "союз" },
        { PartSpeech.Adverb, "нареч" },
        { PartSpeech.Preposition, "предл" },
        { PartSpeech.Interjection, "межд" }
    };

    public static List<string> GetConvertedSpeeches(IEnumerable<PartSpeech> speeches)
    {
        return speeches.Select(speech => converter[speech]).ToList();
    }
}