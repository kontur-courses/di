namespace TagsCloudContainer.Interfaces
{
    public interface ISpeechPartsFilter
    {
        string[] GetInterestingSpeechParts(string[] speechParts);
    }
}