using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.UI
{
    public interface IUserInterface
    {
        void Run(IEnumerable<string> args);
    }
}
