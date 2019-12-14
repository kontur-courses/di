using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions
{
    public abstract class PaletteSetAction : IVisualizerAction
    {
        protected readonly AppSettings appSettings;

        public PaletteSetAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public abstract string GetActionDescription();

        public abstract string GetActionName();

        protected abstract Palette GetPalette();

        public void Perform()
        {
            var newPalette = GetPalette();
            appSettings.Palette = newPalette;
            if (appSettings.CurrentFile != null)
            {
                var newImage = appSettings.CurrentInterface.GetTagCloud();
                appSettings.ImageHolder.SetImage(newImage);
            }
        }
    }
}