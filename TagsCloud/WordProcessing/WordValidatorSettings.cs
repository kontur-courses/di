using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.WordProcessing
{
    public class WordValidatorSettings
    {
        public readonly string pathToBoringWords;

        public WordValidatorSettings(TagCloudSettings tagCloudSettings)
        {
            pathToBoringWords = tagCloudSettings.PathToBoringWords;
        }
    }
}
