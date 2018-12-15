using TagsCloudContainer.UI;

namespace TagsCloudContainer.Filtering
{
    public class BlacklistSettings
    {
        public IBoringWordsRepository BoringWordsRepository { get; }

        public BlacklistSettings(IUI ui)
        {
            BoringWordsRepository = new BoringWordsRepository(ui.ApplicationSettings.FilterSettings.BlacklistPath);
        }
    }
}