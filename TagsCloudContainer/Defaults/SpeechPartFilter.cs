using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class SpeechPartFilter : IWordFilter
{
    private readonly SpeechPartFilterSettings settings;
    private readonly MyStem.MyStem myStem;

    public SpeechPartFilter(SpeechPartFilterSettings settings, MyStem.MyStem myStem)
    {
        this.settings = settings;
        this.myStem = myStem;
    }

    public bool IsValid(string word)
    {
        var part = myStem.AnalyzeWord(word)?.SpeechPart;
        return part.HasValue && !settings.ToFilterOut.Contains(part.Value);
    }
}
