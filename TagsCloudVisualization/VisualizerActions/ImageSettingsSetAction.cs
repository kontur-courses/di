using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions
{
    public abstract class ImageSettingsSetAction : IVisualizerAction
    {
        protected readonly AppSettings appSettings;

        public ImageSettingsSetAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public abstract string GetActionDescription();

        public abstract string GetActionName();

        protected abstract ImageSettings GetImageSettings();

        public void Perform()
        {
            var newImageSettings = GetImageSettings();
            appSettings.ImageSettings = newImageSettings;
            appSettings.ImageHolder.SetImageSize(appSettings.ImageSettings);
            if (appSettings.CurrentFile != null)
            {
                var newImage = appSettings.CurrentInterface.GetTagCloud();
                appSettings.ImageHolder.SetImage(newImage);
            }
        }
    }
}