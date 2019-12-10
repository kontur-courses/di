using System.Collections.Generic;
using TagCloud.Models;

namespace TagCloud.IServices
{
    public interface ITagCollectionFactory
    {
        List<Tag> Create(ImageSettings imageSettings,string path);
    }
}