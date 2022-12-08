using System.ComponentModel;
using MyStemWrapper.Domain;

namespace TagCloudCreatorExtensions.WordsFilters.Settings;

public class FiltersSettings : ISpeechPartWordsFilterSettings, IWordsLengthFilterSettings
{
    [DisplayName("Excluded speech parts")]
    [Description("Excluded parts of speech")]
    [Browsable(false)]
    // Настройка частей речи отключена, так как насколько я понял либо отобразить редактор коллекции
    // в Property grid либо крайне проблематично, либо невозможно совсем(
    public ISet<SpeechPart> ExcludedSpeechParts { get; set; } = new HashSet<SpeechPart>()
    {
        SpeechPart.Interjection,
        SpeechPart.Numeral,
        SpeechPart.Particle,
        SpeechPart.Preposition,
        SpeechPart.Union,
        SpeechPart.PronominalAdjective,
        SpeechPart.PronominalAdverb,
        SpeechPart.Pronoun
    };

    [DisplayName("Exclude undefined speech part")]
    [Description("Exclude words with undefined parts of speech")]
    public bool ExcludeUndefined { get; set; } = false;

    [DisplayName("Minimal word length")]
    [Description("Minimal word length")]
    public int MinWordLength { get; set; } = 0;

    [DisplayName("Max word length")]
    [Description("Max word length")]
    public int MaxWordLength { get; set; } = 20;
}