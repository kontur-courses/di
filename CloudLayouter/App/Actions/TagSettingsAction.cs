using CloudLayouter.Infrastructer.Common;
using CloudLayouter.Infrastructer.Common.Settings;
using CloudLayouter.Infrastructer.Interfaces;

namespace CloudLayouter.Actions
{
    public class TagSettingsAction : IUiAction
    {
        private readonly TagSettings tagSettings;

        public TagSettingsAction(TagSettings tagSettings)
        {
            this.tagSettings = tagSettings;
        }

        public string Name => "Tag settings";
        public string Description => "Установить размер тэгов...";

        public void Perform()
        {
            SettingsForm.For(tagSettings).ShowDialog();
        }

        public MenuCategory Category => MenuCategory.DrawTagCloud;
    }
}