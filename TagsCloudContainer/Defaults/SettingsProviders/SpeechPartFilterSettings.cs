using Mono.Options;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.MyStem;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class SpeechPartFilterSettings : ICliSettingsProvider
{
    private readonly HashSet<SpeechPart> toFilterOut = new()
    {
        SpeechPart.CONJ,
        SpeechPart.INTJ,
        SpeechPart.PART,
        SpeechPart.PR,
    };

    public IReadOnlySet<SpeechPart> ToFilterOut => toFilterOut;

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {"add-parts=",$"Add speech parts to exclusion filter. Defaults to {string.Join(", ",toFilterOut)}",v=>AddParts(v) },
            {"remove-parts=",$"Remove speech parts from exclusion filter. Defaults to {string.Join(", ",toFilterOut)}",v=>RemoveParts(v) }
        };

        return options;
    }

    private void AddParts(string v)
    {
        foreach (var part in v.Split())
        {
            toFilterOut.Add(ParsePart(part));
        }
    }

    private void RemoveParts(string v)
    {
        foreach (var part in v.Split())
        {
            toFilterOut.Remove(ParsePart(part));
        }
    }

    private static SpeechPart ParsePart(string part)
    {
        return Enum.Parse<SpeechPart>(part, true);
    }
}
