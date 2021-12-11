using System.Collections.Generic;

namespace TagsCloudContainer.FileReader
{
    public interface IFileReadersResolver
    {
        IFileReader Get(string fileExt);

    }
}