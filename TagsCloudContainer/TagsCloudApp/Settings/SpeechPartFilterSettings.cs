using System.Collections.Generic;
using System.Linq;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class SpeechPartFilterSettings : ISpeechPartFilterSettings
    {
        public HashSet<SpeechPart> SpeechPartsToRemove { get; }

        public SpeechPartFilterSettings(IRenderOptions renderOptions, IEnumParser parser)
        {
            SpeechPartsToRemove = renderOptions.IgnoredSpeechParts
                .Select(parser.Parse<SpeechPart>)
                .ToHashSet();
        }
    }
}