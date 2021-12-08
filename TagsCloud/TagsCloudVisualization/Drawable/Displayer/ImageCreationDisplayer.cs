using System.Collections.Generic;
using TagsCloudDrawer;
using TagsCloudDrawer.ImageCreator;

namespace TagsCloudVisualization.Drawable.Displayer
{
    public class ImageCreationDisplayer : IDrawableDisplayer
    {
        private readonly string _filename;
        private readonly IImageCreator _creator;

        public ImageCreationDisplayer(string filename, IImageCreator creator)
        {
            _filename = filename;
            _creator = creator;
        }

        public void Display(IEnumerable<IDrawable> drawables) => _creator.Create(_filename, drawables);
    }
}