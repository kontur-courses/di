using TagCloud.TextFilter;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class BlacklistAction : IUiAction
    {
        private readonly CloudPainter cloudPainter;
        private readonly BlacklistMaker blacklistMaker;

        public BlacklistAction(CloudPainter cloudPainter, BlacklistMaker blacklistMaker)
        {
            this.cloudPainter = cloudPainter;
            this.blacklistMaker = blacklistMaker;
        }

        public string Category => "Черный список";
        public string Name => "Настроить";
        public string Description => "Настроить черный список";

        public void Perform()
        {
            SettingsForm<BlacklistMaker>.For(blacklistMaker).ShowDialog();
            cloudPainter.ResetWordsFrequenciesDictionary();
        }
    }
}