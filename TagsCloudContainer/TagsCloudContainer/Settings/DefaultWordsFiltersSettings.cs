using System.Collections.Generic;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public interface IWordsFiltersSettings
    {
        List<IWordsFilter> Filters { get; set; }
    }

    public class DefaultWordsFiltersSettings : IWordsFiltersSettings
    {
        public List<IWordsFilter> Filters { get; set; }

        public DefaultWordsFiltersSettings(IWordSpeechPartParser wordSpeechPartParser)
        {
            Filters = new List<IWordsFilter>
            {
                new SpeechPartWordsFilter(wordSpeechPartParser,
                    new HashSet<SpeechPart>
                    {
                        SpeechPart.INTJ, SpeechPart.PART, SpeechPart.PR, SpeechPart.CONJ
                    })
            };
        }
    }
}