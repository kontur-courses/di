using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualisation.App.DrawingSettings;

namespace TagsCloudVisualisation.App.CloudCreatorDriver.DrawingSettings
{
    public class DrawingSettings : IDrawingSettings
    {
        public Color BgColor { get; set; }
        public List<IWordVisualisation> Visualisation { get; }
        public Size PictureSize { get; set; }

        public DrawingSettings(List<IWordVisualisation> visualisation)
        {
            Visualisation = visualisation;
        }
    }
}