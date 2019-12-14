using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions
{
    public abstract class FontSetAction : IVisualizerAction
    {
        protected readonly AppSettings appSettings;

        public FontSetAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public abstract string GetActionDescription();

        public abstract string GetActionName();

        protected abstract Font GetFont();

        public void Perform()
        {
            var newFont = GetFont();
            appSettings.Font = newFont;
            if (appSettings.CurrentFile != null)
            {
                var newImage = appSettings.CurrentInterface.GetTagCloud();
                appSettings.ImageHolder.SetImage(newImage);
            }
        }
    }
}