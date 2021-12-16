using System.Collections.Generic;

namespace TagsCloudContainerCore.InterfacesCore;

public interface ITagCloudMaker<in TSettings>
{
    public IEnumerable<TagToRender> GetTagsToRender(IEnumerable<string> tags, TSettings settings);
}