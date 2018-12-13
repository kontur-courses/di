using System.Drawing;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class TagsCloudGeneratorSettings
    {
        public Size LetterSize;
        public readonly ITagsCloudLayouter TagsCloudLayouter;
        public readonly IWordsSizer WordsSizer;

        public TagsCloudGeneratorSettings
            (Size letterSize, ITagsCloudLayouter layouter, IWordsSizer sizer)
        {
            LetterSize = letterSize;
            TagsCloudLayouter = layouter;
            WordsSizer = sizer;
        }

        public TagsCloudGeneratorSettings
            (IUI ui, ITagsCloudLayouter layouter, IWordsSizer sizer)
        {
            LetterSize = ui.LetterSize;
            TagsCloudLayouter = layouter;
            WordsSizer = sizer;
        }
    }
}