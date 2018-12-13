using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.Filter
{
    public class BoringWordsFilterSettings : IBoringWordsFilterSettings
    {
        public string BoringWordsFileName { get; }

        public BoringWordsFilterSettings(IConfiguration configuration)
        {
            BoringWordsFileName = configuration.BoringWordsFileName;
        }
    }
}