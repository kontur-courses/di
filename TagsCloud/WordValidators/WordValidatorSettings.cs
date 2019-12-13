namespace TagsCloud.WordValidators
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
