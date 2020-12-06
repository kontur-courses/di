using TagsCloud.Common;
using TagsCloud.Core;

namespace TagsCloud.ClientGUI.Infrastructure
{
    public class TagsCloudFactory : ITagsCloudFactory
    {
        private readonly ISpiralFactory spiralFactory;

        public TagsCloudFactory(ISpiralFactory spiralFactory)
        {
            this.spiralFactory = spiralFactory;
        }

        public ICircularCloudLayouter Create()
        {
            return new CircularCloudLayouter(spiralFactory.Create());
        }
    }
}
