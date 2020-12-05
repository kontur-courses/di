using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class MyStemSpeechPartsFilter : ISpeechPartsFilter
    {
        private readonly HashSet<string> _boringSpeechParts;

        public MyStemSpeechPartsFilter(string[] boringSpeechPartsByMyStem)
        {
            _boringSpeechParts = boringSpeechPartsByMyStem.ToHashSet();
        }

        public string[] GetInterestingSpeechParts(string[] speechParts) =>
            speechParts.Where(speechPart => !_boringSpeechParts.Contains(speechPart)).ToArray();
    }
}