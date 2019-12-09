using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Drawers
{
    public abstract class WordDrawer
    {
        protected AppSettings appSettings;

        public WordDrawer(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public abstract Bitmap GetDrawnLayoutedWords(PaintedWord[] layoutedWords);
    }
}