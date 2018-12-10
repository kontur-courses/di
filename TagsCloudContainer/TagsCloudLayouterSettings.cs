using System.Drawing;

namespace TagsCloudContainer
{
    public class TagsCloudLayouterSettings
    {
        public Point Center { get; }

        public TagsCloudLayouterSettings(IUI ui)
        {
            Center = ui.TagsCloudCenter;
        }
    }
}