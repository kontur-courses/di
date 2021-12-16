using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainerCore.InterfacesCore;

public interface ITagMaker<in TSettings>

{
    public TagToRender MakeTag(KeyValuePair<string, int> raw, TSettings settings, IStatisticMaker statisticMaker);
}