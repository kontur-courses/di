using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public interface IUi
    {
        void CreateImage(IEnumerable<string> args);
    }
}
