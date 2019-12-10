using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.WordProcessing
{
    public class WordValidatorSettings
    {
        public readonly string pathToBoringWords;
        public readonly string[] ignoringPartsOfSpeech;

        public WordValidatorSettings(TagCloudSettings tagCloudSettings)
        {
            pathToBoringWords = tagCloudSettings.PathToBoringWords;
            ignoringPartsOfSpeech = tagCloudSettings.ignoredPartOfSpeech;
        }
    }
}
