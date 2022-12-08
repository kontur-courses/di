using MyStemWrapper.Domain;

namespace TagCloudCreatorExtensions.WordsFilters.Settings;

public interface ISpeechPartWordsFilterSettings
{
    ISet<SpeechPart> ExcludedSpeechParts { get; set; }
    bool ExcludeUndefined { get; set; }
}