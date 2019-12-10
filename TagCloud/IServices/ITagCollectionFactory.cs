using TagCloud.Models;

namespace TagCloud.IServices
{
    public interface ITagCollectionFactory
    {
        TagCollection Create(ImageSettings imageSettings,string path);
    }
}