using System.Drawing;

namespace TagCloud.Creators 
{
    public class TagCreatorFactory : ITagCreatorFactory
    {
        public ITagCreator Create(Font font)
        {
            return new TagCreator(font);
        }
    }
}
