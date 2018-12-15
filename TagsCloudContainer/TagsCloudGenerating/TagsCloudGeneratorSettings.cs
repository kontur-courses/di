using System.Drawing;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.TagsCloudGenerating
{
    public class TagsCloudGeneratorSettings
    {
        public Size LetterSize;
        public readonly ITagsCloudLayouter TagsCloudLayouter;
        public readonly IWordsSizer WordsSizer;


        public TagsCloudGeneratorSettings
            (IUI ui, ITagsCloudLayouter layouter, IWordsSizer sizer)
        {
            LetterSize = ui.ApplicationSettings.ImageSettings.LetterSize;
            TagsCloudLayouter = layouter;
            WordsSizer = sizer;
        }
    }
}