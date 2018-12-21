using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IContainersCreator
    {
        List<ContainerInfo> ContainersInfo { get; }
    }
}
