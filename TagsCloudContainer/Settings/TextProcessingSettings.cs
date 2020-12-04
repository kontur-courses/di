using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class TextProcessingSettings : ITextProcessingSettings
    {
        public string[] BoringWords { get; }

        public TextProcessingSettings(string[] boringWords)
        {
            BoringWords = boringWords;
        }
    }
}