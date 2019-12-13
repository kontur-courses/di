using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class RestrictionsSettingsAction : IGuiAction
    {
        private readonly AppSettings appSettings;

        public RestrictionsSettingsAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GetActionDescription()
        {
            return "Наложить ограничения";
        }

        public string GetActionName()
        {
            return "Ограничения...";
        }

        public void Perform()
        {
            SettingsForm.For(appSettings.Restrictions).ShowDialog();
            if (appSettings.CurrentFile != null)
            {
                var newImage = appSettings.CurrentInterface.GetTagCloud();
                appSettings.ImageHolder.SetImage(newImage);
            }
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}